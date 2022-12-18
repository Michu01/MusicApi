using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.Models
{
    public class AddGenre
    {
        [NotNull]
        [Required]
        [MaxLength(32)]
        public string? Name { get; set; }
    }
}
