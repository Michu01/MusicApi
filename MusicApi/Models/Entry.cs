using System.Diagnostics.CodeAnalysis;

using MusicApi.Enums;

namespace MusicApi.Models
{
    public class Entry
    {
        public Guid Id { get; set; }

        public EntryType Type { get; set; }

        [NotNull]
        public string? Key { get; set; }
    }
}
