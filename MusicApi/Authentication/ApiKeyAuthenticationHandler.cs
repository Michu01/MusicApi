using System.Security.Claims;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

using MusicApi.DbContexts;
using MusicApi.DTOs;

namespace MusicApi.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly MusicDbContext dbContext;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            MusicDbContext dbContext) 
            : base(options, logger, encoder, clock)
        {
            this.dbContext = dbContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyDefaults.Header, out StringValues value) || value[0] is not string apiKey)
            {
                return AuthenticateResult.Fail("Invalid header");
            }

            if (await dbContext.Users.SingleOrDefaultAsync(u => u.ApiKey == apiKey) is not UserDTO user)
            {
                return AuthenticateResult.Fail("Invalid key");
            }

            AuthenticationTicket ticket = CreateTicket(user);

            return AuthenticateResult.Success(ticket);
        }

        private AuthenticationTicket CreateTicket(UserDTO user)
        {
            List<Claim> claims = new()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName)
            };

            if (user.Email != null)
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }

            ClaimsIdentity identity = new(claims, Scheme.Name);
            ClaimsPrincipal principal = new(identity);
            AuthenticationTicket ticket = new(principal, Scheme.Name);

            return ticket;
        }
    }
}
