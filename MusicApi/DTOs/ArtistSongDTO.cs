using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using MusicApi.Models;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(ArtistId), nameof(SongId))]
    public class ArtistSongDTO
    {
        public Guid ArtistId { get; set; }

        [JsonIgnore]
        public virtual ArtistDTO? Artist { get; set; }

        public Guid SongId { get; set; }

        [JsonIgnore]
        public virtual SongDTO? Song { get; set; }

        public ArtistSongDTO() { }

        public ArtistSongDTO(ArtistSong artistSong)
        {
            ArtistId = artistSong.ArtistId;
            SongId = artistSong.SongId;
        }
    }
}
