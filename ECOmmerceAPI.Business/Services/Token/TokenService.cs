using ECommerceAPI.Base;
using ECommerceAPI.Base.Helper;
using ECommerceAPI.Data;
using ECommerceAPI.Schema.DataSets.Token;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Business.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly JwtConfig jwtConfig;
        public TokenService(IUnitOfWork unitOfWork, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            this.unitOfWork = unitOfWork;
            this.jwtConfig = jwtConfig.CurrentValue;
        }


        public ApiResponse<TokenResponse> GetToken(TokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Email) || string.IsNullOrWhiteSpace(request?.Password))
            {
                return new ApiResponse<TokenResponse>("Bad Request");

            }

            var user = unitOfWork.Repository<User>().Where(u => u.Email.Equals(request.Email)).FirstOrDefault();

            if (user is null)
            {
                return new ApiResponse<TokenResponse>("Email or password is incorrect.");
            }

            var passwordHash = JwtHelper.CreateMD5(request.Password);

            if (user.Password != passwordHash)
            {
                return new ApiResponse<TokenResponse>("Email or password is incorrect.");
            }

            var accessToken = Token(user);

            var response = new TokenResponse
            {
                AccessToken = accessToken,
                Email = request.Email,
                ExpireTime = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
            };

            return new ApiResponse<TokenResponse>(response);
        }

        private string Token(User user)
        {
            Claim[] claims = GetClaims(user);
            var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }

        private Claim[] GetClaims(User user)
        {
            var claims = new[]
            {
            new Claim("UserId",user.Id.ToString()),
            new Claim("FirstName",user.UserName),
            new Claim("LastName",user.LastName),
            new Claim("Role",user.Role),
            new Claim("Status",user.Status.ToString()),
            new Claim(ClaimTypes.Role,user.Role),
            new Claim(ClaimTypes.Name,$"{user.UserName} {user.LastName}"

            )
        };

            return claims;
        }

    }
}
