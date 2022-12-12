using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Identity;

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

        public Guid CreatorId { get; set; }

        public virtual UserDTO? Creator { get; set; } 

        public virtual ICollection<SongDTO>? Songs { get; set; }
    }
}
