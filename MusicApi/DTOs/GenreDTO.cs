using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using MusicApi.Models;

namespace MusicApi.DTOs
{
    public class GenreDTO
    {
        public Guid Id { get; set; }

        [NotNull]
        [Required]
        [MaxLength(32)]
        public string? Name { get; set; }

        public DateTime AddedAt { get; set; }

        public GenreDTO() { }

        public GenreDTO(AddGenre genre)
        {
            Patch(genre);
        }

        public void Patch(AddGenre genre)
        {
            Name = genre.Name;
        }
    }
}
