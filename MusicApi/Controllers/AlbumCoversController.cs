using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MusicApi.Authentication;
using MusicApi.DTOs;
using MusicApi.Enums;
using MusicApi.Extensions;
using MusicApi.Services;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumCoversController : ControllerBase
    {
        private readonly AlbumCoverFileManager fileManager;

        public AlbumCoversController(AlbumCoverFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (fileManager.Get(id) is not (FileStream file, string contentType))
            {
                return NotFound();
            }

            return File(file, contentType);
        }

        [HttpPost("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Post(Guid id, IFormFile file)
        {
            string path = await fileManager.Save(id, file);

            return Created(path, null);
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
