using MusicApi.DTOs;

namespace MusicApi.Models
{
    public class ArtistAlbum
    {
        public Guid ArtistId { get; set; }

        public Guid AlbumId { get; set; }
    }
}
