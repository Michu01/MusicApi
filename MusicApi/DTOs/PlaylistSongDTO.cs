using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(PlaylistId), nameof(SongId))]
    public class PlaylistSongDTO
    {
        public Guid PlaylistId { get; set; }

        public virtual PlaylistDTO? Playlist { get; set; }

        public Guid SongId { get; set; }

        public virtual SongDTO? Song { get; set; }

        public DateTime AddedAt { get; set; }
    }
}