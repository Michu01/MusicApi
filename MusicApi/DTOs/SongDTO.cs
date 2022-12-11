using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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

        public Guid? AlbumId { get; set; }

        public virtual AlbumDTO? Album { get; set; }

        public virtual ICollection<ArtistDTO>? Artists { get; set; }

        public virtual ICollection<PlaylistDTO>? Playlists { get; set; }
    }
}