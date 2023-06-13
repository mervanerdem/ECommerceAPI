using ECommerceAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Service.RestExtension;

public static class DbContextExtension
{
    public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
    {
        var dbType = Configuration.GetConnectionString("DbType");
        if (dbType == "SQL")
        {
            var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<EFContext>(opts =>
            opts.UseSqlServer(dbConfig, b => b.MigrationsAssembly("ECommerceAPI.Service")));
        }


    }
}
