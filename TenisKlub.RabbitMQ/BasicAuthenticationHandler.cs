using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Services.Services.Interfaces;

namespace TenisKlub.RabbitMQ;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        protected readonly IUserService _userService;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserService korisniciService)
            : base(options, logger, encoder, clock)
        {
            _userService = korisniciService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header missing");
            }

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                var user = await _userService.Login(username, password);
                if (user == null)
                {
                    return AuthenticateResult.Fail("Invalid username or password");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (FormatException)
            {
                return AuthenticateResult.Fail("Invalid Authorization header format");
            }
            catch (Exception ex)
            {
                // Log exception details here for further investigation
                return AuthenticateResult.Fail("An error occurred processing your authentication.");
            }
        }

    }
