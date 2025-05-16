using Projeto.AuthService.DataTransfer.Usuarios.Request;
using Projeto.AuthService.Dominio.Usuarios.Entidades;
using System;
using System.Threading;
using System.Threading.Tasks;
using Projeto.AuthService.Dominio.Usuarios.enumeradores;

namespace Projeto.AuthService.Aplicacao.Usuarios.Interfaces
{
    public interface IUsuarioAppServico
    {
        Task CadastrarUsuarioAsync(UsuarioRequest usuario, CancellationToken cancellation = default);
        Task DeletaUsuarioAsync(Guid guid, CancellationToken cancellation);
        Task<UsuarioSessao> LoginUsuarioAsync(UsuarioLoginRequest request, CancellationToken cancellation = default);
        Task LogoutUsuarioAsync();
        Task<Usuario> AtribuirRoleUsuarioAsync(Guid guid, RoleEnum role, CancellationToken cancellationToken);
    }
}
