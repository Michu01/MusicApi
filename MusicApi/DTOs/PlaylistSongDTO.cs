using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(PlaylistId), nameof(SongId))]
    public class PlaylistSongDTO
    {
        public Guid PlaylistId { get; set; }

        [JsonIgnore]
        public virtual PlaylistDTO? Playlist { get; set; }

        public Guid SongId { get; set; }

        [JsonIgnore]
        public virtual SongDTO? Song { get; set; }

        public DateTime AddedAt { get; set; }
    }
}