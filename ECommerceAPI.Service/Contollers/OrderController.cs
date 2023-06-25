using ECommerceAPI.Base;
using ECommerceAPI.Base.Helper;
using ECommerceAPI.Business.Services;
using ECommerceAPI.Schema.DataSets.Order;
using ECommerceAPI.Service.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Service.Contollers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [UserRole]
        public ApiResponse AddOrder(OrderRequest orderRequest)
        {
            var userId = JwtHelper.GetUserIdFromContext(HttpContext);
            var response = orderService.Add(orderRequest, userId);
            return response;
        }
        [HttpGet("GetAllMyOrder")]
        [UserRole]
        public ApiResponse<List<OrderResponse>> GetAllMyOrder()
        {
            var userId = JwtHelper.GetUserIdFromContext(HttpContext);
            var response = orderService.GetAllMyOrder(userId);
            return response;
        }

    }
}
