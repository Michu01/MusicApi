using MusicApi.DTOs;
using MusicApi.Enums;

namespace MusicApi.Services
{
    public static class EntryTypeResolver
    {
        public static EntryType Get(object o)
        {
            return o switch
            {
                AlbumDTO => EntryType.Album,
                ArtistDTO => EntryType.Artist,
                UserDTO => EntryType.User,
                SongDTO => EntryType.Song,
                PlaylistDTO => EntryType.Playlist,
                GenreDTO => EntryType.Genre,
                _ => throw new ArgumentException("Invalid object type", nameof(o))
            };
        }
    }
}
