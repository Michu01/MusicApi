using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

using MusicApi.Enums;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(UserId), nameof(EntryId), nameof(EntryType))]
    public class UserFavouriteEntryDTO
    {
        public Guid UserId { get; set; }

        public virtual UserDTO? User { get; set; }

        public Guid EntryId { get; set; }

        public EntryType EntryType { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
