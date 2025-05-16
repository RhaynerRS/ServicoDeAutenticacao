using LocacaoApp.AuthService.Aplicacao.Usuarios.Interfaces;
using LocacaoApp.AuthService.DataTransfer.Usuarios.Request;
using LocacaoApp.AuthService.Dominio.Usuarios.Entidades;
using LocacaoApp.AuthService.Dominio.Usuarios.enumeradores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocacaoApp.AuthService.API.Controllers.Usuarios;

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
    public async Task<IActionResult> CadastrarUsuarioAsync([FromBody] UsuarioRequest usuarioRequest,
        CancellationToken cancellationToken = default)
    {
        await usuarioAppServico.CadastrarUsuarioAsync(usuarioRequest, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Faz login de um usuário existente
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<UsuarioSessao>> UsuarioLoginAsync([FromBody] UsuarioLoginRequest usuarioRequest,
        CancellationToken cancellationToken = default)
    {
        return Ok(await usuarioAppServico.LoginUsuarioAsync(usuarioRequest, cancellationToken));
    }

    /// <summary>
    ///     Faz logout de usuário
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> UsuarioLogoutAsync()
    {
        await usuarioAppServico.LogoutUsuarioAsync();
        return Ok("Logout realizado com sucesso.");
    }

    /// <summary>
    ///     Exclui um usuário permanentemente
    /// </summary>
    [HttpDelete("{Guid}")]
    [Authorize(Roles = "AdminRoleUser")]
    public async Task<IActionResult> UsuarioDeleteAsync([FromRoute] Guid Guid)
    {
        await usuarioAppServico.DeletaUsuarioAsync(Guid);
        return Ok("Usuário excluido com sucesso.");
    }

    /// <summary>
    ///     Faz logout de usuário
    /// </summary>
    [HttpPatch("{Guid}")]
    [Authorize(Roles = "AdminRoleUser")]
    public async Task<IActionResult> AtribuirRoleUsuarioAsync([FromRoute] Guid Guid, [FromQuery] RoleEnum role,
        CancellationToken cancellationToken)
    {
        var usuario = await usuarioAppServico.AtribuirRoleUsuarioAsync(Guid, role, cancellationToken);
        return Ok(usuario);
    }

    /// <summary>
    ///     Valida se a sessao do usuario esta ativa para autenticar outros servicos
    /// </summary>
    [HttpGet("validar")]
    [Authorize]
    public IActionResult ValidaToken()
    {
        return Ok();
    }
}