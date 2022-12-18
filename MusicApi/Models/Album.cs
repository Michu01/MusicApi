using MusicApi.DTOs;
using MusicApi.Enums;

namespace MusicApi.Models
{
    public class Album
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public AlbumType Type { get; set; }

        public DateTime? ReleasedAt { get; set; }

        public DateTime AddedAt { get; set; }

        public Album(AlbumDTO dto)
        {
            Id = dto.Id;
            Title = dto.Title;
            Type = dto.Type;
            ReleasedAt = dto.ReleasedAt;
            AddedAt = dto.AddedAt;
        }
    }
}
