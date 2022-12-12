using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using MusicApi.Enums;

namespace MusicApi.DTOs
{
    public class AlbumDTO
    {
        public Guid Id { get; set; }

        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Title { get; set; }

        public AlbumType Type { get; set; }

        public DateTime? ReleasedAt { get; set; }

        public DateTime AddedAt { get; set; }

        public virtual ICollection<SongDTO>? Songs { get; set; }

        public virtual ICollection<ArtistDTO>? Artists { get; set; }
    }
}
