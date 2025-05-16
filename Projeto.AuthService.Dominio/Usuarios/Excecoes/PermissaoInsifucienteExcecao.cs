using System;

namespace Projeto.AuthService.Dominio.Usuarios.Excecoes
{
    public class PermissaoInsifucienteExcecao : SystemException
    {
        public PermissaoInsifucienteExcecao(string? message)
            : base(message)
        {
        }
    }
}
