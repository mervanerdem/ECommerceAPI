using EcommerceAPI.Service.RestExtension;
using ECommerceAPI.Base;
using ECommerceAPI.Data;
using ECommerceAPI.Service.RestExtension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ECommerceAPI.Service
{
    public class Startup
    {
        //programın şaha kalktığı yer
        public IConfiguration Configuration { get; }
        public static JwtConfig JwtConfig { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            //services.AddControllersWithViews(options =>
            //options.CacheProfiles.Add(ResponseCasheType.Minute45, new CacheProfile
            //{
            //    Duration = 45 * 60,
            //    NoStore = false,
            //    Location = ResponseCacheLocation.Any
            //}));
            services.AddResponseCompression();
            services.AddMemoryCache();
            //services.AddRedisExtension(Configuration);
            services.AddCustomSwaggerExtension();
            services.AddDbContextExtension(Configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMapperExtension();
            services.AddRepositoryExtension();
            services.AddServiceExtension();
            //services.AddJwtExtension();
            //services.AddHangfireExtension(Configuration);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DartVader Company");
                c.DocumentTitle = "DartVader Company";
            });

            //DI
            //app.AddExceptionHandler();
            //app.AddDIExtension();
            //app.UseHangfireDashboard();

            //app.UseMiddleware<HeartBeatMiddleware>();
            //app.UseMiddleware<ErrorHandlerMiddleware>();
            //Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
            //{
            //    Log.Information("-------------Request-Begin------------");
            //    Log.Information(requestProfilerModel.Request);
            //    Log.Information(Environment.NewLine);
            //    Log.Information(requestProfilerModel.Response);
            //    Log.Information("-------------Request-End------------");
            //};
            //app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

            app.UseHttpsRedirection();

            // add auth 
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
