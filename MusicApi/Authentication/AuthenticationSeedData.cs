using Microsoft.AspNetCore.Identity;
using MusicApi.DTOs;
using MusicApi.Services;

namespace MusicApi.Authentication
{
    public class AuthenticationSeedData
    {
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        private readonly UserManager<UserDTO> userManager;

        private readonly IApiKeyService apiKeyService;

        public AuthenticationSeedData(
            RoleManager<IdentityRole<Guid>> roleManager, 
            UserManager<UserDTO> userManager, 
            IApiKeyService apiKeyService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.apiKeyService = apiKeyService;
        }

        public async Task Initialize()
        {
            if (!await roleManager.RoleExistsAsync(RoleDefaults.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = RoleDefaults.Admin });
            }

            if (await userManager.FindByNameAsync(UserDefaults.AdminUserName) is null)
            {
                UserDTO admin = new() { UserName = UserDefaults.AdminUserName, ApiKey = apiKeyService.Generate() };
                await userManager.CreateAsync(admin, UserDefaults.AdminPassword);
                await userManager.AddToRoleAsync(admin, RoleDefaults.Admin);
            }
        }
    }
}
