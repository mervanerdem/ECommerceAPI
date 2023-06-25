using ECommerceAPI.Business.Services.Admin;
using ECommerceAPI.Business.Services.Token;
using ECommerceAPI.Business.Users;

namespace ECommerceAPI.Service.RestExtension
{
    public static class ServiceExtension
    {
        public static void AddServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAdminService, AdminService>();
        }
    }
}
