using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Authentication;
using MusicApi.DbContexts;

using MusicApi.DTOs;
using MusicApi.Extensions;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistSongsController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        public PlaylistSongsController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ArtistSong>> Post(PlaylistSong playlistSong)
        {
            if (await dbContext.Playlists.FindAsync(playlistSong.PlaylistId) is not PlaylistDTO playlist ||
                await dbContext.Songs.FindAsync(playlistSong.SongId) is null)
            {
                return NotFound();
            }

            if (!User.CanAccess(playlist))
            {
                return Forbid();
            }

            PlaylistSongDTO playlistSongDTO = new(playlistSong);

            dbContext.PlaylistSongs.Add(playlistSongDTO);
            await dbContext.SaveChangesAsync();

            return Created(string.Empty, playlistSong);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid playlistId, Guid songId)
        {
            if (await dbContext.PlaylistSongs.FindAsync(playlistId, songId) is not PlaylistSongDTO playlistSongDTO)
            {
                return NotFound();
            }

            await dbContext.Entry(playlistSongDTO).Reference(p => p.Playlist).LoadAsync();

            if (!User.CanAccess(playlistSongDTO.Playlist!))
            {
                return Forbid();
            }

            dbContext.PlaylistSongs.Remove(playlistSongDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
