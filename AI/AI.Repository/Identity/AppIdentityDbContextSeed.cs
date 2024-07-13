using AI.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AI.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Islam Kliyeb",
                    Email = "eslamklyep2019@gmail.com",
                    UserName = "islam_kliyeb",
                    PhoneNumber = "01121130319",
                };
                await userManager.CreateAsync(User, "@Eman157");
            }
        }

    }
}
