using MusicApi.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.Models
{
    public class Genre
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime AddedAt { get; set; }

        public Genre(GenreDTO dto) 
        {
            Id = dto.Id;
            Name = dto.Name;
            AddedAt = dto.AddedAt;
        }
    }
}
