using Projeto.AuthService.DataTransfer;
using Projeto.AuthService.Dominio.Usuarios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projeto.AuthService.Dominio.Usuarios.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> BuscarAsync(Guid guid);
        Task<Usuario> BuscarAsync(Expression<Func<Usuario, string>> func, string atributo);
        Task DeletarAsync(Guid guid, CancellationToken cancellationToken = default);
        Task InserirAsync(Usuario usuario, CancellationToken cancellationToken = default);
        Task EditarAsync(Usuario usuario, CancellationToken cancellationToken = default);
        Task<PaginacaoConsulta<Usuario>> ListarAsync(CancellationToken cancellationToken = default);
        string RedisBuscar(string chave);
        Task RedisDeletarAsync(string chave);
        void RedisInserir(string chave, string valor);
    }
}
