using ECommerceAPI.Base;
using ECommerceAPI.Business.Services.Token;
using ECommerceAPI.Business.Users;
using ECommerceAPI.Schema.DataSets.Token;
using ECommerceAPI.Schema.DataSets.User;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Service
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly IUserService userService;


        public LoginController(ITokenService tokenService, IUserService userService)
        {
            this.tokenService = tokenService;
            this.userService = userService;
        }

        //Kullanıcı register işlemi
        [HttpPost("Register")]
        public ApiResponse Register([FromBody] UserRequest request)
        {
            var response = userService.Insert(request);
            return response;
        }

        //Token alma yeri
        [HttpPost("Login")]
        public ApiResponse<TokenResponse> Login([FromBody] TokenRequest request)
        {
            return tokenService.GetToken(request);
        }
    }
}
