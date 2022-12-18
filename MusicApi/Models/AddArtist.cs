using MusicApi.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.Models
{
    public class AddArtist
    {
        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Name { get; set; }
    }
}
