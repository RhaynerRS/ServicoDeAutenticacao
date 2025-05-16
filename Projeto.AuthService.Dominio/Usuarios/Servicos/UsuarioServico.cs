using Projeto.AuthService.Dominio.Usuarios.Entidades;
using Projeto.AuthService.Dominio.Usuarios.enumeradores;
using Projeto.AuthService.Dominio.Usuarios.Interfaces;
using Projeto.AuthService.Dominio.Usuarios.Repositorios;
using System;
using System.Threading;
using System.Threading.Tasks;
using Projeto.AuthService.Dominio.Usuarios.Excecoes;

namespace Projeto.AuthService.Dominio.Usuarios.Servicos
{
    public class UsuarioServico: IUsuarioServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public bool ValidaSenha(string senha, string senhaCriptografada)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaCriptografada); ;
        }

        public async Task VerificaDuplicidadeEmailAsync(string email)
        {
            Usuario usuarioEmailRepetido = await usuarioRepositorio.BuscarAsync(u => u.Email, email);

            if (usuarioEmailRepetido != null)
                throw new DuplicidadeDeAtributosExecao("E-mail já utilizado por outro usuário!");
        }

        public void VerificaViabilidadeDaRequisicao(string solicitanteRole, bool verificaAutoReferencia=false, Guid? guid=null, string solicitante=null, string mensagemExcecao=null)
        {
            if (guid.ToString() == solicitante && verificaAutoReferencia)
                throw new Exception(mensagemExcecao);

            if (solicitanteRole != RoleEnum.AdminRoleUser.ToString())
                throw new PermissaoInsifucienteExcecao("Usuário solicitante não possui permissão para executar a requisição");
        }

        public async Task<Usuario> VerificaExistenciaUsuarioAsync(Guid guid)
        {
            Usuario usuario = await usuarioRepositorio.BuscarAsync(guid);

            if (usuario == null)
                throw new Exception("Não foi possivel encontrar o usuário desejado");

            return usuario;
        }

        public async Task AtribuirRoleUsuarioAsync(Usuario usuario, RoleEnum role, CancellationToken cancellationToken)
        {
            if (usuario.Role == RoleEnum.AdminRoleUser.ToString())
                throw new PermissaoInsifucienteExcecao("Não é possivel editar as roles de um administrador");
            
            usuario.Role = role.ToString();
            
            await usuarioRepositorio.EditarAsync(usuario, cancellationToken);
        }
    }
}
