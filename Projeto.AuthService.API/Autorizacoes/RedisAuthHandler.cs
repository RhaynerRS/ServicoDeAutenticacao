using Projeto.AuthService.Dominio.Usuarios.Entidades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

public class RedisAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IDatabase _redis;

    public RedisAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IConnectionMultiplexer redis)
        : base(options, logger, encoder, clock)
    {
        _redis = redis.GetDatabase();
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return AuthenticateResult.NoResult();
        }

        var token = authHeader.Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();

        try
        {
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Subject;
            var jti = jwt.Id;

            var key = $"session:{userId}:{jti}";
            var exists = await _redis.KeyExistsAsync(key);

            if (!exists)
                return AuthenticateResult.Fail("Sessão inválida");

            RedisValue result = await _redis.StringGetAsync(key);
            string resultado  = result.ToString();
            UsuarioSessao? sessao = JsonSerializer.Deserialize<UsuarioSessao>(result.ToString());

            DateTime expiracao = DateTimeOffset.FromUnixTimeSeconds(sessao.ExpiresAt).UtcDateTime;

            if (expiracao < DateTime.UtcNow)
                throw new Exception("Token vencido");

            var identity = new ClaimsIdentity(jwt.Claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch
        {
            return AuthenticateResult.Fail("Token inválido");
        }
    }
}
