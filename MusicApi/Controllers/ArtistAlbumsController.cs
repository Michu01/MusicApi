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
    public class ArtistAlbumsController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        public ArtistAlbumsController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<ActionResult<ArtistAlbum>> Post(ArtistAlbum artistAlbum)
        {
            ArtistAlbumDTO artistAlbumDTO = new(artistAlbum);

            dbContext.ArtistAlbums.Add(artistAlbumDTO);
            await dbContext.SaveChangesAsync();

            return Created(string.Empty, artistAlbum);
        }

        [HttpDelete]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Delete(Guid artistId, Guid albumId)
        {
            if (await dbContext.ArtistAlbums.FindAsync(artistId, albumId) is not ArtistAlbumDTO artistAlbumDTO)
            {
                return NotFound();
            }

            dbContext.ArtistAlbums.Remove(artistAlbumDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
