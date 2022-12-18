using System.Security.Claims;

using MusicApi.DTOs;

namespace MusicApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? TryGetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier) is string s &&
                Guid.TryParse(s, out Guid id)
                ? id
                : null;
        }

        public static Guid GetId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static bool CanAccess(this ClaimsPrincipal user, PlaylistDTO playlist) 
        {
            return !playlist.IsPrivate || user.TryGetId() == playlist.CreatorId;
        }
    }
}
