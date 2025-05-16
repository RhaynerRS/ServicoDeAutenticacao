using Projeto.AuthService.Dominio.Usuarios.Entidades;
using System;
using System.Threading;
using System.Threading.Tasks;
using Projeto.AuthService.Dominio.Usuarios.enumeradores;

namespace Projeto.AuthService.Dominio.Usuarios.Interfaces
{
    public interface IUsuarioServico
    {
        bool ValidaSenha(string senha, string senhaCriptografada);
        Task VerificaDuplicidadeEmailAsync(string email);
        Task<Usuario> VerificaExistenciaUsuarioAsync(Guid guid);
        void VerificaViabilidadeDaRequisicao(string solicitanteRole, bool verificaAutoReferencia = false, Guid? guid = null, string solicitante = null, string mensagemExcecao = null);
        Task AtribuirRoleUsuarioAsync(Usuario usuario, RoleEnum role, CancellationToken cancellationToken);
    }
}
