using ECommerceAPI.Base;
using ECommerceAPI.Data.Domain;
using ECommerceAPI.Schema.DataSets.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Business.Services
{
    public interface IOrderService : IBaseService<Order, OrderRequest, OrderResponse>
    {
        ApiResponse Add(OrderRequest request, int userId);

        ApiResponse<List<OrderResponse>> GetAllMyOrder(int userId);


    }
}
