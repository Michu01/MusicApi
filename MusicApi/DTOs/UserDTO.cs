using Microsoft.AspNetCore.Identity;

namespace MusicApi.DTOs
{
    public class UserDTO : IdentityUser<Guid>
    {
        public DateTime JoinedAt { get; set; }

        public virtual ICollection<UserFavouriteEntryDTO>? FavouriteEntries { get; set; }

        public virtual ICollection<UserPlayedEntryDTO>? RecentlyPlayed { get; set; }

        public virtual ICollection<PlaylistDTO>? Playlists { get; set; }

        public virtual ICollection<UserFollowDTO>? UserFollows { get; set; }
    }
}
