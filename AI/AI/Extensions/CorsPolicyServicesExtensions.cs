using AI.Repository.Data;
using AI.Repository.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace AI.Extensions
{
    public static class CorsPolicyServicesExtensions
    {
        public static IServiceCollection AddCorsPolicyServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(Options =>
            {
                Options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyOrigin();
                });
            });
            return services;
        }
    }
}
