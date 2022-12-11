using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.DTOs
{
    public class GenreDTO
    {
        public Guid Id { get; set; }

        [NotNull]
        [Required]
        [MaxLength(32)]
        public string? Name { get; set; }

        public virtual ICollection<SongDTO>? Songs { get; set; }

        public virtual ICollection<AlbumDTO>? Albums { get; set; }

        public virtual ICollection<ArtistDTO>? Artists { get; set; }

        public virtual ICollection<PlaylistDTO>? Playlists { get; set; }
    }
}
