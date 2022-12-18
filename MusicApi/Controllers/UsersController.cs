using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

using MusicApi.Authentication;
using MusicApi.DbContexts;
using MusicApi.DTOs;
using MusicApi.Extensions;
using MusicApi.Models;
using MusicApi.Services;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MusicDbContext dbContext;

        private readonly UserManager<UserDTO> userManager;

        public UsersController(UserManager<UserDTO> userManager, MusicDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IQueryable<User> Get(string? userNamePattern, int offset = 0, int limit = 10)
        {
            userNamePattern ??= "";
            limit = Math.Min(limit, 100);

            IQueryable<User> users = dbContext.Users
                .Where(a => a.UserName.Contains(userNamePattern))
                .Skip(offset * limit)
                .Take(limit)
                .Select(a => new User(a));

            return users;
        }

        [HttpGet(nameof(Me))]
        [Authorize]
        public async Task<ActionResult<User>> Me()
        {
            UserDTO user = await userManager.FindByIdAsync(User);

            return new User(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            if (await userManager.FindByIdAsync(id) is not UserDTO user)
            {
                return NotFound();
            }

            return new User(user);
        }
    }
}
