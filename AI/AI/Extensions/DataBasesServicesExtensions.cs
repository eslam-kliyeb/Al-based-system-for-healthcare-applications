using AI.Repository.Data;
using AI.Repository.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace AI.Extensions
{
    public static class DataBasesServicesExtensions
    {
        public static IServiceCollection AddDataBasesServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(sql => {
                sql.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            });
            services.AddDbContext<AppIdentityDbContext>(sql =>
            {
                sql.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection"));
                return ConnectionMultiplexer.Connect(config);
            });
            return services;
        }
    }
}
