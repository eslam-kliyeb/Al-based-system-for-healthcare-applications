using AI.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AI.Core.Interfaces.Service
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
    }
}
