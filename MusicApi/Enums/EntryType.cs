using System.Text.Json.Serialization;

namespace MusicApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EntryType
    {
        Artist, Album, Song, Playlist, User, Genre
    }
}
