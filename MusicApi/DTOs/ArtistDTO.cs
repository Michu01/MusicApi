using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using MusicApi.Models;

namespace MusicApi.DTOs
{
    public class ArtistDTO
    {
        public Guid Id { get; set; }

        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Name { get; set; }

        public DateTime AddedAt { get; set; }

        [JsonIgnore]
        public virtual ICollection<SongDTO> Songs { get; set; } = new List<SongDTO>();

        [JsonIgnore]
        public virtual ICollection<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();

        public ArtistDTO() { }

        public ArtistDTO(AddArtist artist)
        {
            Patch(artist);
        }

        public void Patch(AddArtist artist)
        {
            Name = artist.Name;
        }
    }
}
