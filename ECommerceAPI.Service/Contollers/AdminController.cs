using ECommerceAPI.Base;
using ECommerceAPI.Business.Services.Admin;
using ECommerceAPI.Business.Users;
using ECommerceAPI.Schema.DataSets.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Schema.DataSets.Admin;

namespace ECommerceAPI.Service.Contollers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService adminService;
        private readonly IUserService userService;


        public AdminController(IAdminService adminService, IUserService userService)
        {
            this.adminService = adminService;
            this.userService = userService;
        }

        [HttpPost("Register")]
        public ApiResponse CreateAdmin([FromBody] UserRequest request)
        {
            var response = adminService.Insert(request);
            return response;
        }

        // Adminlerin kullanıcı silmesi için kullanılır.
        [HttpDelete("DeleteUser")]
        public ApiResponse DeleteAdmin(int userId)
        {
            var response = userService.Delete(userId);
            return response;
        }

        [HttpPut("UpdateUser")]
        public ApiResponse UpdateUser(int userId, AdminRequest request)
        {
            var response = userService.UpdateUserByAdmin(userId, request);
            return response;
        }

        [HttpGet("GetAllUser")]
        public ApiResponse<List<AdminResponse>> GetAllUser()
        {
            var response = userService.GetAllUser();
            return response;
        }

    }
}
