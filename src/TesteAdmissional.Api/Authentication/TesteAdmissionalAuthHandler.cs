using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace TesteAdmissional.Api.Authentication;

public class TesteAdmissionalAuthHandler : AuthenticationHandler<TesteAdmissionalAuthSchemeOptions>
{
    private readonly TesteAdmissionalAuthKeyKeeper _keyKeeper;

    public TesteAdmissionalAuthHandler(IOptionsMonitor<TesteAdmissionalAuthSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, TesteAdmissionalAuthKeyKeeper testeAdmissionalAuthKeyKeeper) : base(
        options, logger, encoder, clock)
    {
        _keyKeeper = testeAdmissionalAuthKeyKeeper;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            return AuthenticateResult.Fail("Authorization header missing!");

        var header = Request.Headers[HeaderNames.Authorization].ToString();
        var tokenMatch = Regex.Match(header, @"^(?i)Bearer (?<Token>.*)");

        if (!tokenMatch.Success) return AuthenticateResult.Fail("Invalid Authorization header.");

        var token = tokenMatch.Groups["Token"].Value;

        try
        {
            var validToken = _keyKeeper.CheckTokenIsValid(token);

            if (!validToken) return AuthenticateResult.Fail("Invalid token");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, token)
            };

            var claimsIdentity = new ClaimsIdentity(claims, nameof(TesteAdmissionalAuthHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception e)
        {
            return AuthenticateResult.Fail(e.Message);
        }
    }
}