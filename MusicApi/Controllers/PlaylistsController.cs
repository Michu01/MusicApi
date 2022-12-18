using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Authentication;
using MusicApi.DbContexts;

using MusicApi.DTOs;
using MusicApi.Extensions;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        public PlaylistsController(MusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IQueryable<Playlist> Get(string? namePattern, int offset = 0, int limit = 10)
        {
            namePattern ??= "";
            limit = Math.Min(limit, 100);

            IQueryable<Playlist> playlists = dbContext.Playlists
                .Where(a => User.CanAccess(a))
                .Where(a => a.Name.Contains(namePattern))
                .Skip(offset * limit)
                .Take(limit)
                .Select(a => new Playlist(a));

            return playlists;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> Get(Guid id)
        {
            if (await dbContext.Playlists.FindAsync(id) is not PlaylistDTO playlistDTO)
            {
                return NotFound();
            }

            if (playlistDTO.IsPrivate && User.TryGetId() != playlistDTO.CreatorId)
            {
                return Forbid();
            }

            return new Playlist(playlistDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Playlist>> Post(AddPlaylist playlist)
        {
            Guid creatorId = User.GetId();

            PlaylistDTO playlistDTO = new(playlist, creatorId);

            dbContext.Playlists.Add(playlistDTO);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = playlistDTO.Id }, new Playlist(playlistDTO));
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Patch(Guid id, AddPlaylist playlist)
        {
            if (await dbContext.Playlists.FindAsync(id) is not PlaylistDTO playlistDTO)
            {
                return NotFound();
            }

            if (User.GetId() != playlistDTO.CreatorId)
            {
                return Forbid();
            }

            playlistDTO.Patch(playlist);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await dbContext.Playlists.FindAsync(id) is not PlaylistDTO playlistDTO)
            {
                return NotFound();
            }

            if (User.GetId() != playlistDTO.CreatorId)
            {
                return Forbid();
            }

            dbContext.Playlists.Remove(playlistDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
