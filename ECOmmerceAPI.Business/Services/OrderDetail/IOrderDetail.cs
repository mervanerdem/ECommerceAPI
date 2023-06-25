using ECommerceAPI.Base;
using ECommerceAPI.Data.Domain;
using ECommerceAPI.Schema.DataSets.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Business.Services
{
    public interface IOrderDetailService : IBaseService<OrderDetail, OrderDetailRequest, OrderDetailResponse>
    {
        public ApiResponse<List<OrderDetailResponse>> GetAllMyOrderDetail(string orderNumber);
    }
}
