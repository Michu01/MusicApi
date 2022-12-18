using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Authentication;
using MusicApi.DbContexts;

using MusicApi.DTOs;
using MusicApi.Enums;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryGenresController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        public EntryGenresController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Post(EntryGenre entryGenre)
        {
            EntryGenreDTO entryGenreDTO = new(entryGenre);

            dbContext.EntryGenres.Add(entryGenreDTO);
            await dbContext.SaveChangesAsync();

            return Created(string.Empty, entryGenre);
        }

        [HttpDelete]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Delete(Guid entryId, GenreEntryType entryType, Guid genreId)
        {
            if (await dbContext.EntryGenres.FindAsync(entryId, entryType, genreId) is not EntryGenreDTO entryGenreDTO)
            {
                return NotFound();
            }

            dbContext.EntryGenres.Remove(entryGenreDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
