using MusicApi.DTOs;

namespace MusicApi.Models
{
    public class PlaylistSong
    {
        public Guid PlaylistId { get; set; }

        public Guid SongId { get; set; }
    }
}
