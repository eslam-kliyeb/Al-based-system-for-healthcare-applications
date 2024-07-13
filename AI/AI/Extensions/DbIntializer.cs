using AI.Core.Entities.Identity;
using AI.Repository.Data;
using AI.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AI.Extensions
{
    public static class DbInitializer
    {
        public static async Task InitializeDbAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<DataContext>();
                    var identityContext = service.GetRequiredService<AppIdentityDbContext>();
                    //var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
                    //Create DB if it does not exist
                    if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();
                    if ((await identityContext.Database.GetPendingMigrationsAsync()).Any()) await identityContext.Database.MigrateAsync();
                    //Apply Seeding
                    //await DataContextSeed.SeedDataAsync(context);
                    var UserManager = service.GetRequiredService<UserManager<AppUser>>();
                    await AppIdentityDbContextSeed.SeedUserAsync(UserManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
