using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MusicApi.DbContexts;
using MusicApi.DTOs;

using MusicApi.Models;

using MusicApi.Services;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserDTO> userManager;

        private readonly IApiKeyService apiKeyService;

        public AuthController(UserManager<UserDTO> userManager, IApiKeyService apiKeyService)
        {
            this.userManager = userManager;
            this.apiKeyService = apiKeyService;
        }

        [HttpPost(nameof(GetApiKey))]
        public async Task<IActionResult> GetApiKey([Required] UserAuthentication authentication)
        {
            if (await userManager.FindByNameAsync(authentication.Name) is not UserDTO user)
            {
                return BadRequest("Invalid username");
            }

            if (!await userManager.CheckPasswordAsync(user, authentication.Password))
            {
                return BadRequest("Invalid password");
            }

            return Ok(user.ApiKey);
        }

        [HttpPost(nameof(SignUp))]
        public async Task<IActionResult> SignUp([Required] CreateUser user)
        {
            UserDTO userDTO = new()
            {
                UserName = user.Name,
                Email = user.Email,
                ApiKey = apiKeyService.Generate()
            };

            IdentityResult identityResult = await userManager.CreateAsync(userDTO, user.Password);

            if (identityResult.Succeeded)
            {
                return CreatedAtAction(nameof(UsersController.Me), new User(userDTO));
            }

            return BadRequest(identityResult.Errors);
        }
    }
}
