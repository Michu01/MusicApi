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
            if (fileManager.Find(id) is not string filename)
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

            (FileStream file, string contentType) = fileManager.Get(filename);

            return File(file, contentType);
        }

        [HttpPost("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Post(Guid id, IFormFile file)
        {
            if (!await dbContext.Songs.AnyAsync(s => s.Id == id))
            {
                return NotFound();
            }

            await fileManager.Save(id, file);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }
    }
}
