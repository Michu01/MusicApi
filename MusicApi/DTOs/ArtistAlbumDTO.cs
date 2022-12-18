using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using MusicApi.Models;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(ArtistId), nameof(AlbumId))]
    public class ArtistAlbumDTO
    {
        public Guid ArtistId { get; set; }

        [JsonIgnore]
        public virtual ArtistDTO? Artist { get; set; }

        public Guid AlbumId { get; set; }

        [JsonIgnore]
        public virtual AlbumDTO? Album { get; set; }

        public ArtistAlbumDTO() { }

        public ArtistAlbumDTO(ArtistAlbum artistAlbum)
        {
            ArtistId = artistAlbum.ArtistId;
            AlbumId = artistAlbum.AlbumId;
        }
    }
}
