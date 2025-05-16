using System;

namespace Projeto.AuthService.Dominio.Usuarios.Excecoes
{
    public class DuplicidadeDeAtributosExecao : SystemException
    {
        public DuplicidadeDeAtributosExecao(string? message)
            : base(message)
        {
        }
    }
}
