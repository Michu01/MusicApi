using Microsoft.EntityFrameworkCore;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(ArtistId), nameof(AlbumId))]
    public class ArtistAlbumDTO
    {
        public Guid ArtistId { get; set; }

        public virtual ArtistDTO? Artist { get; set; }

        public Guid AlbumId { get; set; }

        public virtual AlbumDTO? Album { get; set; }
    }
}
