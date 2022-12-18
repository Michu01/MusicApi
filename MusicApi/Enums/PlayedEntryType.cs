using System.Text.Json.Serialization;

using MusicApi.Models;

namespace MusicApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PlayedEntryType
    {
        Song, Playlist
    }
}
