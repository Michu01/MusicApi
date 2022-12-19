using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MusicApi.Authentication;
using MusicApi.DbContexts;
using MusicApi.DTOs;
using MusicApi.Services;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumSongsController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        public AlbumSongsController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Post(Guid id, [Required] Guid songId)
        {
            if (await dbContext.Albums.FindAsync(id) is not AlbumDTO albumDTO ||
                await dbContext.Songs.FindAsync(songId) is not SongDTO songDTO)
            {
                return NotFound();
            }

            albumDTO.Songs.Add(songDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Delete(Guid id, [Required] Guid songId)
        {
            if (await dbContext.Albums.FindAsync(id) is not AlbumDTO albumDTO ||
                await dbContext.Songs.FindAsync(songId) is not SongDTO songDTO)
            {
                return NotFound();
            }

            albumDTO.Songs.Remove(songDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
