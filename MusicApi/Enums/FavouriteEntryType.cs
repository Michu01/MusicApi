using System.Text.Json.Serialization;

namespace MusicApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FavouriteEntryType
    {
        Artist, Album, Song, Playlist, Genre
    }
}
