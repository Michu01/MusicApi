using System.Drawing.Imaging;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MusicApi.DbContexts;
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
        public IActionResult Get(Guid id)
        {
            if (fileManager.Find(id) is not string filename)
            {
                return NotFound();
            }

            (FileStream file, string contentType) = fileManager.Get(filename);

            return File(file, contentType);
        }

        [HttpPost("{id}")]
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
