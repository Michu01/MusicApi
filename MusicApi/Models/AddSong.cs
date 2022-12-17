using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.Models
{
    public class AddSong
    {
        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Title { get; set; }

        public DateTime? ReleasedAt { get; set; }

        public Guid? AlbumId { get; set; }
    }
}
