using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Identity;

using MusicApi.Models;

namespace MusicApi.DTOs
{
    public class PlaylistDTO
    {
        public Guid Id { get; set; }

        [NotNull]
        [Required]
        [MaxLength(64)]
        public string? Name { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsPrivate { get; set; }

        public Guid CreatorId { get; set; }

        [JsonIgnore]
        public virtual UserDTO? Creator { get; set; }

        [JsonIgnore]
        public virtual ICollection<SongDTO> Songs { get; set; } = new List<SongDTO>();

        public PlaylistDTO() { }

        public PlaylistDTO(AddPlaylist playlist, Guid creatorId)
        {
            CreatorId = creatorId;
            Patch(playlist);
        }

        public void Patch(AddPlaylist playlist)
        {
            Name = playlist.Name;
            Description = playlist.Description;
            IsPrivate = playlist.IsPrivate;
        }
    }
}
