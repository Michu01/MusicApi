using System.Drawing.Imaging;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MusicApi.Authentication;
using MusicApi.DbContexts;
using MusicApi.DTOs;
using MusicApi.Enums;
using MusicApi.Extensions;
using MusicApi.Services;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongFilesController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        private readonly ISongFileManager fileManager;

        public SongFilesController(MusicDbContext dbContext, ISongFileManager fileManager)
        {
            this.dbContext = dbContext;
            this.fileManager = fileManager;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            if (fileManager.Get(id) is not (FileStream file, string contentType))
            {
                return NotFound();
            }

            Guid userId = User.GetId();

            UserPlayedEntryDTO userPlayedEntry = new()
            {
                UserId = userId,
                EntryId = id,
                EntryType = PlayedEntryType.Song
            };

            dbContext.UserPlayedEntries.Add(userPlayedEntry);
            await dbContext.SaveChangesAsync();

            return File(file, contentType);
        }

        [HttpPost("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Post(Guid id, IFormFile file)
        {
            await fileManager.Save(id, file);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public IActionResult Delete(Guid id)
        {
            if (!fileManager.Exists(id))
            {
                return NotFound();
            }

            fileManager.Delete(id);

            return NoContent();
        }
    }
}
