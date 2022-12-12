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
        private readonly UserManager<UserDTO> userManager;

        public UsersController(UserManager<UserDTO> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet(nameof(Me))]
        [Authorize]
        public async Task<ActionResult<User>> Me()
        {
            UserDTO user = await userManager.FindByIdAsync(User);

            return Ok(new User(user));
        }
    }
}
