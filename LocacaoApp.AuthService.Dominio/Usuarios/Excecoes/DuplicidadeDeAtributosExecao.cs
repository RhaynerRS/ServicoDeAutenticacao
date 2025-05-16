using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoApp.AuthService.Dominio.Usuarios.Excecoes
{
    public class DuplicidadeDeAtributosExecao : SystemException
    {
        public DuplicidadeDeAtributosExecao(string? message)
            : base(message)
        {
        }
    }
}
