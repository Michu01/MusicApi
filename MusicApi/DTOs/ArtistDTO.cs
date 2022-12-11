using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.DTOs
{
    public class ArtistDTO
    {
        public Guid Id { get; set; }

        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Name { get; set; }

        public virtual ICollection<SongDTO>? Songs { get; set; }

        public virtual ICollection<AlbumDTO>? Albums { get; set; }
    }
}
