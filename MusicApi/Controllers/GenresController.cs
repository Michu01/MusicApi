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
    public class GenresController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        public GenresController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IQueryable<Genre> Get(string? namePattern, int offset = 0, int limit = 10)
        {
            namePattern ??= "";
            limit = Math.Min(limit, 100);

            IQueryable<Genre> genres = dbContext.Genres
                .Where(a => a.Name.Contains(namePattern))
                .Skip(offset * limit)
                .Take(limit)
                .Select(a => new Genre(a));

            return genres;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(Guid id)
        {
            if (await dbContext.Genres.FindAsync(id) is not GenreDTO genreDTO)
            {
                return NotFound();
            }

            return new Genre(genreDTO);
        }

        [HttpPost]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<ActionResult<Genre>> Post(AddGenre genre)
        {
            GenreDTO genreDTO = new(genre);

            dbContext.Genres.Add(genreDTO);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = genreDTO.Id }, new Genre(genreDTO));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Patch(Guid id, AddGenre genre)
        {
            if (await dbContext.Genres.FindAsync(id) is not GenreDTO genreDTO)
            {
                return NotFound();
            }

            genreDTO.Patch(genre);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await dbContext.Genres.FindAsync(id) is not GenreDTO genreDTO)
            {
                return NotFound();
            }

            dbContext.Genres.Remove(genreDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
