using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Authentication;
using MusicApi.DbContexts;

using MusicApi.DTOs;
using MusicApi.Enums;
using MusicApi.Models;
using MusicApi.Services;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        private readonly ISongFileManager songFileManager;

        public ArtistsController(MusicDbContext dbContext, ISongFileManager songFileManager)
        {
            this.dbContext = dbContext;
            this.songFileManager = songFileManager;
        }

        [HttpGet]
        public IQueryable<Artist> Get(string? namePattern, int offset = 0, int limit = 10)
        {
            namePattern ??= "";
            limit = Math.Min(limit, 100);

            IQueryable<Artist> artists = dbContext.Artists
                .Where(a => a.Name.Contains(namePattern))
                .Skip(offset * limit)
                .Take(limit)
                .Select(a => new Artist(a));

            return artists;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> Get(Guid id)
        {
            if (await dbContext.Artists.FindAsync(id) is not ArtistDTO artistDTO)
            {
                return NotFound();
            }

            return new Artist(artistDTO);
        }

        [HttpGet("{id}/Albums")]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums(Guid id)
        {
            if (await dbContext.Artists.FindAsync(id) is not ArtistDTO artistDTO)
            {
                return NotFound();
            }

            await dbContext.Entry(artistDTO).Collection(a => a.Albums).LoadAsync();

            IEnumerable<Album> albums = artistDTO.Albums.Select(a => new Album(a));

            return Ok(albums);
        }

        [HttpGet("{id}/Songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs(Guid id)
        {
            if (await dbContext.Artists.FindAsync(id) is not ArtistDTO artistDTO)
            {
                return NotFound();
            }

            await dbContext.Entry(artistDTO).Collection(a => a.Songs).LoadAsync();

            IEnumerable<Song> songs = artistDTO.Songs.Select(a => new Song(a, songFileManager));

            return Ok(songs);
        }

        [HttpGet("{id}/Genres")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres(Guid id)
        {
            if (await dbContext.Artists.FindAsync(id) is not ArtistDTO artistDTO)
            {
                return NotFound();
            }

            IEnumerable<Genre> genres = dbContext.EntryGenres
                .Where(e => e.EntryType == GenreEntryType.Artist)
                .Where(e => e.EntryId == id)
                .Join(dbContext.Genres, e => e.GenreId, e => e.Id, (e1, e2) => e2)
                .Select(g => new Genre(g));

            return Ok(genres);
        }

        [HttpPost]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<ActionResult<Artist>> Post(AddArtist artist)
        {
            ArtistDTO artistDTO = new(artist);

            dbContext.Artists.Add(artistDTO);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = artistDTO.Id }, new Artist(artistDTO));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Patch(Guid id, AddArtist artist)
        {
            if (await dbContext.Artists.FindAsync(id) is not ArtistDTO artistDTO)
            {
                return NotFound();
            }

            artistDTO.Patch(artist);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await dbContext.Artists.FindAsync(id) is not ArtistDTO artistDTO)
            {
                return NotFound();
            }

            dbContext.Artists.Remove(artistDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
