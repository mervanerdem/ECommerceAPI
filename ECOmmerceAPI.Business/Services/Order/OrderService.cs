using AutoMapper;
using ECommerceAPI.Base;
using ECommerceAPI.Data.Domain;
using ECommerceAPI.Data;
using ECommerceAPI.Schema.DataSets.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Base.Helper;

namespace ECommerceAPI.Business.Services
{

    public class OrderService : BaseService<Category, OrderRequest, OrderResponse>, IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // Sipariş Oluşturmak için kullanılır.
        public ApiResponse Add(OrderRequest request, int userId)
        {
            if (request is null)
            {
                return new ApiResponse("Bad Request");
            }
            decimal pointSum = 0;
            decimal couponPercentAmount = 0;
            string couponCod = "";
            var products = unitOfWork.Repository<Product>().Where(x => request.ProductIds.Contains(x.Id));
            var productPrices = products.Select(x => x.Price).Sum();
            var productCount = products.Count();
            if (StockControl(products))
            {
                return new ApiResponse("No Stock");
            }
            var user = unitOfWork.Repository<User>().GetByIdAsNoTracking(userId);
            var userPointRatio = user.DigitalWallet / productCount;
            decimal productsPrice = 0;
            if (string.IsNullOrWhiteSpace(request.CouponCode))
            {
                foreach (var item in products)
                {
                    pointSum += PointCalculator(item, userPointRatio, item.Price);
                    productsPrice += item.Price;
                }
            }
            else
            {
                var coupon = unitOfWork.Repository<Coupon>().Where(x => x.Code.Equals(request.CouponCode)).FirstOrDefault();
                if (CouponControl(coupon))
                {
                    return new ApiResponse("Coupon Wrong");
                }
                couponPercentAmount = coupon.PercentAmount;
                couponCod = coupon.Code;
                foreach (var item in products)
                {
                    var price = item.Price - ((coupon.PercentAmount * item.Price) / 100);
                    pointSum += PointCalculator(item, userPointRatio, price);
                    productsPrice += price;
                }
            }
            var sumPrice = productsPrice - user.DigitalWallet;
            var couponAmount = (productPrices * couponPercentAmount) / 100;
            if (sumPrice > 0)
            {
                if (Payment(sumPrice))
                {
                    var orderNumber = GenerateUniqueOrderNumber();
                    OrderAdd(userId, sumPrice, user.DigitalWallet, couponAmount, couponCod, orderNumber);
                    PointAdd(user, pointSum);
                    StockReduction(products);
                    OrderDetailAdd(products, orderNumber);
                    if (unitOfWork.Complete() > 0)
                    {
                        return new ApiResponse();
                    }
                    else
                    {
                        return new ApiResponse("Internal Server Error");
                    }
                }
                return new ApiResponse("Payment Failed.");
            }
            else
            {
                return new ApiResponse();
            }
        }

        private string GenerateUniqueOrderNumber()
        {
            while (true)
            {
                var orderNumber = JwtHelper.GenerateOrderNumber();
                if (!unitOfWork.Repository<Order>().Where(x => x.OrderNumber.Equals(orderNumber)).Any())
                {
                    return orderNumber;
                }
            }
        }

        private void OrderDetailAdd(IQueryable<Product> products, string orderNumber)
        {
            foreach (var item in products)
            {
                unitOfWork.Repository<OrderDetail>().Add(new OrderDetail
                {
                    OrderNumber = orderNumber,
                    Price = item.Price,
                    ProductId = item.Id,
                    ProductName = item.Name
                });

            }
        }
        private void StockReduction(IQueryable<Product> products)
        {
            foreach (var item in products)
            {
                item.Stock--;
                unitOfWork.Repository<Product>().Update(item);
            }
        }

        private void OrderAdd(int userId, decimal sumPrice, decimal pointBalance, decimal couponAmount, string couponCode, string orderNumber)
        {
            unitOfWork.Repository<Order>().Add(new Order
            {
                UserID = userId,
                OrderNumber = orderNumber,
                CartAmount = sumPrice,
                PointAmount = pointBalance,
                CouponAmount = couponAmount,
                CouponCode = couponCode
            });
        }

        private bool CouponControl(Coupon coupon)
        {
            if (coupon is null)
            {
                return true;
            }
            if (coupon.IsActive.Equals(false) || DateTime.Now > coupon.ExpirationDate)
            {
                return true;
            }
            return false;

        }

        private bool StockControl(IQueryable<Product> products)
        {
            foreach (var item in products)
            {
                if (item.Stock < 1)
                {
                    return true;
                }
            }
            return false;
        }

        private void PointAdd(User user, decimal pointSum)
        {
            user.DigitalWallet = pointSum;
            unitOfWork.Repository<User>().Update(user);
        }

        private bool Payment(decimal sumPrice)
        {
            return true;

        }

        private decimal PointCalculator(Product item, decimal userPointRatio, decimal price)
        {
            var newPrice = price - userPointRatio;
            var percentage = (newPrice * item.PointEarningPercentage) / 100;
            var point = percentage > item.MaxPointAmount ? item.MaxPointAmount : percentage;
            return point;
        }

        public ApiResponse<List<OrderResponse>> GetAllMyOrder(int userId)
        {
            var orders = unitOfWork.Repository<Order>().Where(x => x.UserID.Equals(userId)).ToList();
            var orderResponses = mapper.Map<List<OrderResponse>>(orders);
            return new ApiResponse<List<OrderResponse>>(orderResponses);

        }
    }
}
