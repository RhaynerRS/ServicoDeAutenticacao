using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoApp.AuthService.Dominio.Usuarios.Excecoes
{
    public class PermissaoInsifucienteExcecao : SystemException
    {
        public PermissaoInsifucienteExcecao(string? message)
            : base(message)
        {
        }
    }
}
