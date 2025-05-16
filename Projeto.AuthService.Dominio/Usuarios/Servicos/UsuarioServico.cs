using Projeto.AuthService.Dominio.Usuarios.Entidades;
using Projeto.AuthService.Dominio.Usuarios.enumeradores;
using Projeto.AuthService.Dominio.Usuarios.Interfaces;
using Projeto.AuthService.Dominio.Usuarios.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.AuthService.Dominio.Usuarios.Servicos
{
    public class UsuarioServico: IUsuarioServico
    {
        public UsuarioServico()
        {
        }

        public bool ValidaSenha(string senha, string senhaCriptografada)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaCriptografada); ;
        }
    }
}
