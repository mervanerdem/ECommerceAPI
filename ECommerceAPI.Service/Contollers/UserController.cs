using ECommerceAPI.Base;
using ECommerceAPI.Base.Helper;
using ECommerceAPI.Business.Users;
using ECommerceAPI.Schema.DataSets.User;
using ECommerceAPI.Service.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Service
{

    [EnableMiddlewareLogger]
    [ResponseGuid]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //User isteklerinin servis edildiği kısım

        private readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize]
        [UserRole]
        public ApiResponse<UserResponse> UserGetBalance()
        {
            var userId = JwtHelper.GetUserIdFromContext(HttpContext);
            var result = service.UserBalance(userId);
            return result;
              
        }

        [HttpPut("UpdateUser")]
        [Authorize]
        [UserRole]
        public ApiResponse UpdateUser([FromBody] UserRequest request)
        {
            var id = JwtHelper.GetUserIdFromContext(HttpContext);
            var response = service.Update(id, request);
            return response;
        }


    }
}
