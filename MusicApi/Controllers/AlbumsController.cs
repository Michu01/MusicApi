using System.ComponentModel.DataAnnotations;

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
    public class AlbumsController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        private readonly ISongFileManager songFileManager;

        public AlbumsController(MusicDbContext dbContext, ISongFileManager songFileManager)
        {
            this.dbContext = dbContext;
            this.songFileManager = songFileManager;
        }

        [HttpGet]
        public IQueryable<Album> Get(string? titlePattern, int offset = 0, int limit = 10)
        {
            titlePattern ??= "";
            limit = Math.Min(limit, 100);

            IQueryable<Album> albums = dbContext.Albums
                .Where(a => a.Title.Contains(titlePattern))
                .Skip(offset * limit)
                .Take(limit)
                .Select(a => new Album(a));

            return albums;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> Get(Guid id)
        {
            if (await dbContext.Albums.FindAsync(id) is not AlbumDTO albumDTO)
            {
                return NotFound();
            }

            return new Album(albumDTO);
        }

        [HttpGet("{id}/Songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs(Guid id)
        {
            if (await dbContext.Albums.FindAsync(id) is not AlbumDTO albumDTO)
            {
                return NotFound();
            }

            await dbContext.Entry(albumDTO).Collection(a => a.Songs).LoadAsync();

            IEnumerable<Song> songs = albumDTO.Songs.Select(a => new Song(a, songFileManager));

            return Ok(songs);
        }

        [HttpGet("{id}/Artists")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists(Guid id)
        {
            if (await dbContext.Albums.FindAsync(id) is not AlbumDTO albumDTO)
            {
                return NotFound();
            }

            await dbContext.Entry(albumDTO).Collection(a => a.Artists).LoadAsync();

            IEnumerable<Artist> artists = albumDTO.Artists.Select(a => new Artist(a));

            return Ok(artists);
        }

        [HttpGet("{id}/Genres")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres(Guid id)
        {
            if (await dbContext.Artists.FindAsync(id) is not ArtistDTO artistDTO)
            {
                return NotFound();
            }

            IEnumerable<Genre> genres = dbContext.EntryGenres
                .Where(e => e.EntryType == GenreEntryType.Album)
                .Where(e => e.EntryId == id)
                .Join(dbContext.Genres, e => e.GenreId, e => e.Id, (e1, e2) => e2)
                .Select(g => new Genre(g));

            return Ok(genres);
        }

        [HttpPost]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<ActionResult<Album>> Post(AddAlbum album)
        {
            AlbumDTO albumDTO = new(album);

            dbContext.Albums.Add(albumDTO);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = albumDTO.Id }, new Album(albumDTO));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Patch(Guid id, AddAlbum album)
        {
            if (await dbContext.Albums.FindAsync(id) is not AlbumDTO albumDTO)
            {
                return NotFound();
            }

            albumDTO.Patch(album);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await dbContext.Albums.FindAsync(id) is not AlbumDTO albumDTO)
            {
                return NotFound();
            }

            dbContext.Albums.Remove(albumDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
