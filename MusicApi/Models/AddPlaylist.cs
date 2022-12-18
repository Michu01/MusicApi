using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.Models
{
    public class AddPlaylist
    {
        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Name { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        public bool IsPrivate { get; set; }
    }
}
