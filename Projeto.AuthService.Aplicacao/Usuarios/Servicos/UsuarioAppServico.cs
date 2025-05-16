using Projeto.AuthService.Aplicacao.Usuarios.Interfaces;
using Projeto.AuthService.DataTransfer.Usuarios.Request;
using Projeto.AuthService.Dominio.Usuarios.Entidades;
using Projeto.AuthService.Dominio.Usuarios.enumeradores;
using Projeto.AuthService.Dominio.Usuarios.Interfaces;
using Projeto.AuthService.Dominio.Usuarios.Repositorios;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Projeto.AuthService.Aplicacao.Usuarios.Servicos
{
    public class UsuarioAppServico: IUsuarioAppServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IUsuarioServico usuarioServico;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UsuarioAppServico(IUsuarioRepositorio usuarioRepositorio, IUsuarioServico usuarioServico, IHttpContextAccessor httpContextAccessor)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.usuarioServico = usuarioServico;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task CadastrarUsuarioAsync(UsuarioRequest usuario, CancellationToken cancellation = default)
        {
            Usuario usuarioParaCadastrar = new(usuario.Name, usuario.Password, RoleEnum.DefaultRoleUser.ToString(), usuario.Email);
            await usuarioRepositorio.InserirAsync(usuarioParaCadastrar, cancellation);
        }

        public async Task<UsuarioSessao> LoginUsuarioAsync(UsuarioLoginRequest request, CancellationToken cancellation = default)
        {
            Usuario usuarioALogar = await usuarioRepositorio.BuscarAsync(u => u.Email, request.Email);

            if (!usuarioServico.ValidaSenha(request.Password, usuarioALogar.Password))
                throw new Exception("Senha incorreta");
            Guid jwtId = Guid.NewGuid();
            UsuarioSessao sessao = new(usuarioALogar.Guid, usuarioALogar.Email, usuarioALogar.Role, jwtId);

            usuarioRepositorio.RedisInserir($"session:{usuarioALogar.Guid}:{jwtId}", JsonSerializer.Serialize(sessao));

            return sessao;
        }

        public async Task LogoutUsuarioAsync()
        {
            string token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Subject;
            var jti = jwt.Id;

            var key = $"session:{userId}:{jti}";
            await usuarioRepositorio.RedisDeletarAsync(key);
        }
    }
}
