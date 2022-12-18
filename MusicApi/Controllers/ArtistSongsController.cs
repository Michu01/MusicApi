using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Authentication;
using MusicApi.DbContexts;

using MusicApi.DTOs;

using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistSongsController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        public ArtistSongsController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<ActionResult<ArtistSong>> Post(ArtistSong artistSong)
        {
            ArtistSongDTO artistSongDTO = new(artistSong);

            dbContext.ArtistSongs.Add(artistSongDTO);
            await dbContext.SaveChangesAsync();

            return Created(string.Empty, artistSong);
        }

        [HttpDelete]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Delete(Guid artistId, Guid albumId)
        {
            if (await dbContext.ArtistSongs.FindAsync(artistId, albumId) is not ArtistSongDTO artistSongDTO)
            {
                return NotFound();
            }

            dbContext.ArtistSongs.Remove(artistSongDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
