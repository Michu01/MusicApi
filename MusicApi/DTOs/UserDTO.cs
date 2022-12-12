using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Identity;

namespace MusicApi.DTOs
{
    public class UserDTO : IdentityUser<Guid>
    {
        [NotNull]
        [Required]
        [StringLength(40, MinimumLength = 40)]
        public string? ApiKey { get; set; }

        public DateTime JoinedAt { get; set; }

        public virtual ICollection<UserFavouriteEntryDTO>? FavouriteEntries { get; set; }

        public virtual ICollection<UserPlayedEntryDTO>? RecentlyPlayed { get; set; }

        public virtual ICollection<PlaylistDTO>? Playlists { get; set; }

        public virtual ICollection<UserFollowDTO>? UserFollows { get; set; }
    }
}
