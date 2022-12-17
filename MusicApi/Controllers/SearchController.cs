using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MusicApi.DbContexts;
using MusicApi.DTOs;
using MusicApi.Models;
using MusicApi.Services;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        public SearchController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<Entry> Get(string? key, int offset = 0, int limit = 10)
        {
            key ??= "";
            limit = Math.Min(limit, 100);

            IEnumerable<Entry> matchingArtists = dbContext.Artists
                .Where(a => a.Name.Contains(key))
                .Select(a => new Entry() { Type = EntryTypeResolver.Get(a), Id = a.Id, Key = a.Name });

            IEnumerable<Entry> matchingAlbums = dbContext.Albums
                .Where(a => a.Title.Contains(key))
                .Select(a => new Entry() { Type = EntryTypeResolver.Get(a), Id = a.Id, Key = a.Title });

            IEnumerable<Entry> matchingGenres = dbContext.Genres
                .Where(g => g.Name.Contains(key))
                .Select(a => new Entry() { Type = EntryTypeResolver.Get(a), Id = a.Id, Key = a.Name });

            IEnumerable<Entry> matchingSongs = dbContext.Songs
                .Where(s => s.Title.Contains(key))
                .Select(a => new Entry() { Type = EntryTypeResolver.Get(a), Id = a.Id, Key = a.Title });

            IEnumerable<Entry> matchingUsers = dbContext.Users
                .Where(u => u.UserName.Contains(key))
                .Select(a => new Entry() { Type = EntryTypeResolver.Get(a), Id = a.Id, Key = a.UserName });

            IEnumerable<Entry> matchingPlaylists = dbContext.Playlists
                .Where(p => p.Name.Contains(key))
                .Select(a => new Entry() { Type = EntryTypeResolver.Get(a), Id = a.Id, Key = a.Name });

            IEnumerable<Entry> matching = Enumerable.Empty<Entry>()
                .Concat(matchingSongs)
                .Concat(matchingArtists)
                .Concat(matchingAlbums)
                .Concat(matchingGenres)
                .Concat(matchingPlaylists)
                .Concat(matchingUsers)
                .Skip(offset * limit)
                .Take(limit);

            return Ok(matching);
        }
    }
}
