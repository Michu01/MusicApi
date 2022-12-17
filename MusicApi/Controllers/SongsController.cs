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
        public ActionResult<IEnumerable<Song>> Get()
        {
            IEnumerable<Song> songs = dbContext.Songs.Select(s => new Song(s, songFileManager));

            return Ok(songs);
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

        [HttpPost]
        public async Task<ActionResult<SongDTO>> Post(AddSong song)
        {
            SongDTO songDTO = new()
            {
                AlbumId = song.AlbumId,
                ReleasedAt = song.ReleasedAt,
                Title = song.Title
            };

            dbContext.Songs.Add(songDTO);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = songDTO.Id }, songDTO);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, AddSong song)
        {
            if (await dbContext.Songs.FindAsync(id) is not SongDTO songDTO)
            {
                return NotFound();
            }

            songDTO.AlbumId = song.AlbumId;
            songDTO.ReleasedAt = song.ReleasedAt;
            songDTO.Title = song.Title;

            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await dbContext.Songs.FindAsync(id) is not SongDTO song)
            {
                return NotFound();
            }

            dbContext.Songs.Remove(song);
            await dbContext.SaveChangesAsync();

            return NoContent();
        } 
    }
}
