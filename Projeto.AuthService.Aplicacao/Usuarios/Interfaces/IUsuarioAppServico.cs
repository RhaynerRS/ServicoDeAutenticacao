using Projeto.AuthService.DataTransfer.Usuarios.Request;
using Projeto.AuthService.Dominio.Usuarios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projeto.AuthService.Aplicacao.Usuarios.Interfaces
{
    public interface IUsuarioAppServico
    {
        Task CadastrarUsuarioAsync(UsuarioRequest usuario, CancellationToken cancellation = default);
        Task<UsuarioSessao> LoginUsuarioAsync(UsuarioLoginRequest request, CancellationToken cancellation = default);
        Task LogoutUsuarioAsync();
    }
}
