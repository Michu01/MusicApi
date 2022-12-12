using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

namespace MusicApi.Extensions
{
    public static class UserManagerExtensions
    {
        public static Task<TUser> FindByIdAsync<TUser>(this UserManager<TUser> userManager, ClaimsPrincipal user)
            where TUser : class
        {
            return userManager.FindByIdAsync(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
