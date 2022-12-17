using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MusicApi.DTOs
{
    public class SongDTO
    {
        public Guid Id { get; set; }

        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Title { get; set; }

        public DateTime? ReleasedAt { get; set; }

        public DateTime AddedAt { get; set; }

        public Guid? AlbumId { get; set; }

        [JsonIgnore]
        public virtual AlbumDTO? Album { get; set; }

        [JsonIgnore]
        public virtual ICollection<ArtistDTO>? Artists { get; set; }

        [JsonIgnore]
        public virtual ICollection<PlaylistDTO>? Playlists { get; set; }
    }
}