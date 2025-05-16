using Projeto.AuthService.Aplicacao.Usuarios.Interfaces;
using Projeto.AuthService.DataTransfer.Usuarios.Request;
using Projeto.AuthService.Dominio.Usuarios.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.AuthService.Dominio.Usuarios.enumeradores;

namespace Projeto.AuthService.API.Controllers.Usuarios
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAppServico usuarioAppServico;
        public UsuarioController(IUsuarioAppServico usuarioAppServico)
        {
            this.usuarioAppServico = usuarioAppServico;
        }

        [HttpPost]
        [Authorize]
        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        public async Task<IActionResult> PingAsync([FromBody]UsuarioRequest usuarioRequest, CancellationToken cancellationToken = default)
        {
            await usuarioAppServico.CadastrarUsuarioAsync(usuarioRequest, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Faz login de um usuário existente
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioSessao>> UsuarioLoginAsync([FromBody]UsuarioLoginRequest usuarioRequest, CancellationToken cancellationToken = default)
        {
            
            return Ok(await usuarioAppServico.LoginUsuarioAsync(usuarioRequest, cancellationToken));
        }
        
        /// <summary>
        /// Faz login de um usuário existente
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> UsuarioLogoutAsync()
        {
            await usuarioAppServico.LogoutUsuarioAsync();
            return Ok("Logout realizado com sucesso.");
        }
        
        /// <summary>
        /// Exclui um usuário existente.
        /// </summary>
        [HttpDelete("{Guid}")]
        [Authorize(Roles = "AdminRoleUser")]
        public async Task<IActionResult> DeletaUsuarioAsync([FromRoute]Guid guid, CancellationToken cancellationToken)
        {
            await usuarioAppServico.DeletaUsuarioAsync(guid, cancellationToken);
            return Ok("O usuário foi excluído com sucesso.");
        }
        
        /// <summary>
        /// Atribui uma nova role a um usuário existente.
        /// </summary>
        [HttpPatch("{Guid}")]
        [Authorize(Roles = "AdminRoleUser")]
        public async Task<IActionResult> AtribuirRoleUsuarioAsync([FromRoute]Guid guid, [FromQuery] RoleEnum role, CancellationToken cancellationToken)
        {
            Usuario usuario = await usuarioAppServico.AtribuirRoleUsuarioAsync(guid,role, cancellationToken);
            return Ok(usuario);
        }

        [HttpGet("validar")]
        [Authorize]
        public IActionResult ValidaToken()
        {
            return Ok();
        }
    }
}
