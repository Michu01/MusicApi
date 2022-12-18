using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.DTOs;
using MusicApi.DbContexts;
using Microsoft.AspNetCore.StaticFiles;
using MusicApi.Models;
using MusicApi.Services;
using Microsoft.AspNetCore.Authorization;
using MusicApi.Authentication;
using MusicApi.Extensions;
using MusicApi.Enums;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        private readonly ISongFileManager songFileManager;

        public SongsController(MusicDbContext dbContext, ISongFileManager songFileManager)
        {
            this.dbContext = dbContext;
            this.songFileManager = songFileManager;
        }

        [HttpGet]
        public IQueryable<Song> Get(string? titlePattern, int offset = 0, int limit = 10)
        {
            titlePattern ??= "";
            limit = Math.Min(limit, 100);

            IQueryable<Song> songs = dbContext.Songs
                .Where(a => a.Title.Contains(titlePattern))
                .Skip(offset * limit)
                .Take(limit)
                .Select(a => new Song(a, songFileManager));

            return songs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> Get(Guid id)
        {
            if (await dbContext.Songs.FindAsync(id) is not SongDTO songDTO)
            {
                return NotFound();
            }

            return new Song(songDTO, songFileManager);
        }

        [HttpGet("RecentlyPlayed")]
        [Authorize]
        public IQueryable<Song> GetRecentlyPlayed(int offset = 0, int limit = 10)
        {
            limit = Math.Min(limit, 100);

            Guid id = User.GetId();

            IQueryable<Song> songs = dbContext.UserPlayedEntries
                .Where(e => e.EntryType == PlayedEntryType.Song)
                .Where(e => e.UserId == id)
                .OrderByDescending(e => e.PlayedAt)
                .Skip(offset * limit)
                .Take(limit)
                .Join(dbContext.Songs, e => e.EntryId, e => e.Id, (e1, e2) => e2)
                .Select(s => new Song(s, songFileManager));

            return songs;
        }

        [HttpPost]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<ActionResult<Song>> Post(AddSong song)
        {
            SongDTO songDTO = new(song);

            dbContext.Songs.Add(songDTO);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = songDTO.Id }, new Song(songDTO, songFileManager));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Patch(Guid id, AddSong song)
        {
            if (await dbContext.Songs.FindAsync(id) is not SongDTO songDTO)
            {
                return NotFound();
            }

            songDTO.Patch(song);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleDefaults.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await dbContext.Songs.FindAsync(id) is not SongDTO song)
            {
                return NotFound();
            }

            dbContext.Songs.Remove(song);
            await dbContext.SaveChangesAsync();

            songFileManager.Delete(id);

            return NoContent();
        } 
    }
}
