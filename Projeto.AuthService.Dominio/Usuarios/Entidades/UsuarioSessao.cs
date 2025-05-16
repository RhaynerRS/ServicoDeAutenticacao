using System.Security.Claims;
using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace Projeto.AuthService.Dominio.Usuarios.Entidades
{
    public class UsuarioSessao
    {
        public string AcessToken { get; set; }
        public long ExpiresAt { get; set; } = new DateTimeOffset(DateTime.UtcNow.AddHours(1)).ToUnixTimeSeconds();
        public Guid RefreshToken { get; set; } = Guid.NewGuid();

        [JsonConstructor]
        private UsuarioSessao() { }

        public UsuarioSessao(Guid guid, string email, string role, Guid JwtId)
        {
            GerarToken(guid, email, role, JwtId);
        }

        private void GerarToken(Guid guid, string email, string role, Guid JwtId)
        {
            var chaveSecreta = Guid.NewGuid().ToString();
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, guid.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, JwtId.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: "sua.api.com",
                audience: "sua.api.com",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credenciais
            );

           AcessToken =  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
