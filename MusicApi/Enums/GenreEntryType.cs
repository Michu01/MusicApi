using System.Text.Json.Serialization;

namespace MusicApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GenreEntryType
    {
        Artist, Album, Song, Playlist
    }
}
