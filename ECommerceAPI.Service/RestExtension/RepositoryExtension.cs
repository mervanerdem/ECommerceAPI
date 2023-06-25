using ECommerceAPI.Business.Services;
using ECommerceAPI.Business.Services.Admin;
using ECommerceAPI.Data.Repository;

namespace ECommerceAPI.Service.RestExtension
{
    public static class RepositoryExtension
    {
        public static void AddRepositoryExtension(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

        }
    }
}
