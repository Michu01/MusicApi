using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using MusicApi.DTOs;
using MusicApi.Enums;

namespace MusicApi.Models
{
    public class AddAlbum
    {
        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Title { get; set; }

        public AlbumType Type { get; set; }

        public DateTime? ReleasedAt { get; set; }
    }
}
