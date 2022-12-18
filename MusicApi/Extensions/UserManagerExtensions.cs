using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using MusicApi.DTOs;

namespace MusicApi.Extensions
{
    public static class UserManagerExtensions
    {
        public static Task<UserDTO> FindByIdAsync(this UserManager<UserDTO> userManager, ClaimsPrincipal user)
        {
            return userManager.FindByIdAsync(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static Task<UserDTO> FindByIdAsync(this UserManager<UserDTO> userManager, Guid id)
        {
            return userManager.FindByIdAsync(id.ToString());
        }
    }
}
