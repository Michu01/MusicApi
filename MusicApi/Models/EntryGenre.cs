using MusicApi.DTOs;
using MusicApi.Enums;

namespace MusicApi.Models
{
    public class EntryGenre
    {
        public Guid EntryId { get; set; }

        public GenreEntryType EntryType { get; set; }

        public Guid GenreId { get; set; }
    }
}
