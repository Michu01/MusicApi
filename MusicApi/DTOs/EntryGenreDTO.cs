using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using MusicApi.Enums;
using MusicApi.Models;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(EntryId), nameof(GenreId))]
    public class EntryGenreDTO
    {
        public Guid EntryId { get; set; }

        public GenreEntryType EntryType { get; set; }

        public Guid GenreId { get; set; }

        [JsonIgnore]
        public virtual GenreDTO? Genre { get; set; }

        public EntryGenreDTO() { }

        public EntryGenreDTO(EntryGenre entryGenre)
        {
            EntryId = entryGenre.EntryId;
            EntryType = entryGenre.EntryType;
            GenreId = entryGenre.GenreId;
        }
    }
}
