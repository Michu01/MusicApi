using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(ArtistId), nameof(SongId))]
    public class ArtistSongDTO
    {
        public Guid ArtistId { get; set; }

        public virtual ArtistDTO? Artist { get; set; }

        public Guid SongId { get; set; }
    
        public virtual SongDTO? Song { get; set; }
    }
}
