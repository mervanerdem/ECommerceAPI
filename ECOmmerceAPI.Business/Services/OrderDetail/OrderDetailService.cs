using AutoMapper;
using ECommerceAPI.Base;
using ECommerceAPI.Data;
using ECommerceAPI.Data.Domain;
using ECommerceAPI.Schema.DataSets.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Business.Services
{
    public class OrderDetailService : BaseService<OrderDetail, OrderDetailRequest, OrderDetailResponse>, IOrderDetailService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public ApiResponse<List<OrderDetailResponse>> GetAllMyOrderDetail(string orderNumber)
        {
            var orders = unitOfWork.Repository<OrderDetail>().Where(x => x.OrderNumber.Equals(orderNumber)).ToList();
            var orderResponses = mapper.Map<List<OrderDetailResponse>>(orders);
            return new ApiResponse<List<OrderDetailResponse>>(orderResponses);

        }
    }
}
