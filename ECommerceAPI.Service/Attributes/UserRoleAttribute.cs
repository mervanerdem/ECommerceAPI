using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ECommerceAPI.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UserRoleAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var jwtHandler = new JwtSecurityTokenHandler();
            if (!jwtHandler.CanReadToken(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var jwtToken = jwtHandler.ReadJwtToken(token);
            var claimsIdentity = new ClaimsIdentity(jwtToken.Claims);

            var expirationDate = jwtToken.ValidTo;
            if (expirationDate < DateTime.UtcNow)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userId = claimsIdentity.FindFirst("UserId")?.Value;
            var userRole = claimsIdentity.FindFirst("Role")?.Value;

            context.HttpContext.Items["UserId"] = userId;
            context.HttpContext.Items["UserRole"] = userRole;
        }

        //ekstra işlemler için
        public void OnActionExecuted(ActionExecutedContext context)
        {    
        }
    }
}
