using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Identity;

using Newtonsoft.Json;

namespace MusicApi.DTOs
{
    public class UserDTO : IdentityUser<Guid>
    {
        [NotNull]
        [Required]
        [StringLength(40, MinimumLength = 40)]
        public string? ApiKey { get; set; }

        public DateTime JoinedAt { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserFavouriteEntryDTO> FavouriteEntries { get; set; } = new List<UserFavouriteEntryDTO>();

        [JsonIgnore]
        public virtual ICollection<UserPlayedEntryDTO> RecentlyPlayed { get; set; } = new List<UserPlayedEntryDTO>();

        [JsonIgnore]
        public virtual ICollection<PlaylistDTO> Playlists { get; set; } = new List<PlaylistDTO>();

        [JsonIgnore]
        public virtual ICollection<UserFollowDTO> UserFollows { get; set; } = new List<UserFollowDTO>();
    }
}
