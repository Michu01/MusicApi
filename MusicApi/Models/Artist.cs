using MusicApi.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.Models
{
    public class Artist
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime AddedAt { get; set; }

        public Artist(ArtistDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            AddedAt = dto.AddedAt;
        }
    }
}
