using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using Microsoft.Identity.Client;

using MusicApi.Enums;
using MusicApi.Models;

namespace MusicApi.DTOs
{
    public class AlbumDTO
    {
        public Guid Id { get; set; }

        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Title { get; set; }

        public AlbumType Type { get; set; }

        public DateTime? ReleasedAt { get; set; }

        public DateTime AddedAt { get; set; }

        [JsonIgnore]
        public virtual ICollection<SongDTO> Songs { get; set; } = new List<SongDTO>();

        [JsonIgnore]
        public virtual ICollection<ArtistDTO> Artists { get; set; } = new List<ArtistDTO>();

        public AlbumDTO() { }

        public AlbumDTO(AddAlbum album)
        {
            Patch(album);
        }

        public void Patch(AddAlbum album)
        {
            Title = album.Title;
            Type = album.Type;
            ReleasedAt = album.ReleasedAt;
        }
    }
}
