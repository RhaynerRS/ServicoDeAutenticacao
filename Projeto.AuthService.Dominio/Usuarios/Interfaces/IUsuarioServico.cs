using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.AuthService.Dominio.Usuarios.Interfaces
{
    public interface IUsuarioServico
    {
        bool ValidaSenha(string senha, string senhaCriptografada);
    }
}
