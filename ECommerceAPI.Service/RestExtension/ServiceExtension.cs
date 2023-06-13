﻿using ECommerceAPI.Business;

namespace ECommerceAPI.Service.RestExtension
{
    public static class ServiceExtension
    {
        public static void AddServiceExtension(this IServiceCollection services)
        {
            //services.AddScoped<IUserLogService, UserLogService>();
            //services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<ICustomerService, CustomerService>();
            //services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<ITransactionService, TransactionService>();
            //services.AddScoped<ICurrencyService, CurrencyService>();
            //services.AddSingleton<IRedisService, RedisService>();

            //services.AddScoped<ITransactionReportService, TransactionReportService>();
            //services.AddScoped<IDapperAccountService, DapperAccountService>();

            //services.AddScoped<ScopedService>();
            //services.AddTransient<TransientService>();
            //services.AddSingleton<SingletonService>();
        }
    }
}
