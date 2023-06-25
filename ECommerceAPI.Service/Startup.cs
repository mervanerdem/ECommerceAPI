using EcommerceAPI.Service.RestExtension;
using ECommerceAPI.Base;
using ECommerceAPI.Data;
using ECommerceAPI.Service.RestExtension;

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

            services.AddCustomSwaggerExtension();
            services.AddDbContextExtension(Configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMapperExtension();
            services.AddRepositoryExtension();
            services.AddServiceExtension();
            services.AddJwtExtension();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            //Migrations dediğin otomatik olur

            IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using IServiceScope serviceScope = serviceScopeFactory.CreateScope();
            EFContext dbContext = serviceScope.ServiceProvider.GetService<EFContext>();
            dbContext.Database.EnsureCreated();


            //Swagger config
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DartVader Company");
                c.DocumentTitle = "DartVader Company"; //Kimse starwars sevgimizi sorgulamasın :D
            });

            //DI
            //app.AddExceptionHandler();
            //app.AddDIExtension();

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
