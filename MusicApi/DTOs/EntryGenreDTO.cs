using Microsoft.EntityFrameworkCore;

using MusicApi.Enums;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(EntryId), nameof(GenreId))]
    public class EntryGenreDTO
    {
        public Guid EntryId { get; set; }

        public EntryType EntryType { get; set; }

        public Guid GenreId { get; set; }

        public virtual GenreDTO? Genre { get; set; }
    }
}
