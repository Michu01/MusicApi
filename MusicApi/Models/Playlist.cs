using MusicApi.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsPrivate { get; set; }

        public Guid CreatorId { get; set; }

        public Playlist(PlaylistDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Description = dto.Description;
            CreatedAt = dto.CreatedAt;
            IsPrivate = dto.IsPrivate;
            CreatorId = dto.CreatorId;
        }
    }
}
