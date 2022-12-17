using System.Text.Json.Serialization;

namespace MusicApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AlbumType
    {
        Album, EP, Single, Compilation
    }
}
