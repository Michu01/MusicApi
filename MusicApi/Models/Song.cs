using MusicApi.DTOs;
using MusicApi.Services;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MusicApi.Models
{
    public class Song
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public DateTime? ReleasedAt { get; set; }

        public DateTime AddedAt { get; set; }

        public Guid? AlbumId { get; set; }

        public string? Duration { get; set; }

        public Song(SongDTO dto, ISongFileManager songFileManager)
        {
            Id = dto.Id;
            Title = dto.Title;
            AddedAt = dto.AddedAt;
            AlbumId = dto.AlbumId;
            ReleasedAt = dto.ReleasedAt;
            Duration = songFileManager.GetDuration(dto.Id)?.ToString();
        }
    }
}
