using ECommerceAPI.Base;
using ECommerceAPI.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Schema;

namespace ECommerceAPI.Service
{

    //[EnableMiddlewareLogger]
    //[ResponseGuid]
    [Route("simapi/v1/[controller]")]
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
      // [Authorize]
        public ApiResponse<List<UserResponse>> GetAll()
        {
            return service.GetAll();
        }

        [HttpGet("{id}")]
      //  [Authorize]
        public ApiResponse<UserResponse> GetById(int id)
        {
            return service.GetById(id);
        }

        [HttpPost]
       // [Authorize]
        public ApiResponse Post([FromBody] UserRequest request)
        {
            var response = service.Insert(request);
            return response;
        }

        [HttpPost("{id}")]
        public ApiResponse UpdateUser(int id, [FromBody] UserRequest request)
        {
            var response = service.Update(id, request);
            return response;
        }


        [HttpDelete("{id}")]
        //[Authorize]
        public ApiResponse Delete(int id)
        {
            var response = service.Delete(id);
            return response; ;
        }


    }
}
