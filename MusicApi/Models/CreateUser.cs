using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.Models
{
    public class CreateUser
    {
        [Required]
        [NotNull]
        [MaxLength(32)]
        public string? Name { get; set; }

        [Required]
        [NotNull]
        [MaxLength(32)]
        public string? Password { get; set; }

        [EmailAddress] 
        public string? Email { get; set; }
    }
}
