using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using MusicApi.Enums;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(UserId), nameof(EntryId), nameof(EntryType), nameof(PlayedAt))]
    public class UserPlayedEntryDTO
    {
        public Guid UserId { get; set; }

        public virtual UserDTO? User { get; set; }

        public Guid EntryId { get; set; }

        public EntryType EntryType { get; set; }

        public DateTime PlayedAt { get; set; }
    }
}
