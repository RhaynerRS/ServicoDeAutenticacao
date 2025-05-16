using Projeto.AuthService.DataTransfer;
using Projeto.AuthService.Dominio.Usuarios.Entidades;
using Projeto.AuthService.Dominio.Usuarios.Repositorios;
using MongoDB.Driver;
using StackExchange.Redis;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Projeto.AuthService.Infra.Usuarios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IMongoCollection<Usuario> database;
        private readonly IDatabase redis;
        public UsuarioRepositorio(IMongoDatabase database, IConnectionMultiplexer connectionMultiplexer)
        {
            this.database = database.GetCollection<Usuario>("Usuario");
            redis = connectionMultiplexer.GetDatabase();
        }

        public async Task InserirAsync(Usuario usuario, CancellationToken cancellationToken = default)
        {
            await database.InsertOneAsync(usuario, cancellationToken);
        }

        public async Task<Usuario> BuscarAsync(Guid guid)
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.Guid, guid);

            using var resultado = await database.FindAsync(filtro);

            return await resultado.FirstOrDefaultAsync();
        }
        
        public async Task<Usuario> BuscarAsync(Expression<Func<Usuario,string>> func,  string atributo)
        {
            var filtro = Builders<Usuario>.Filter.Eq(func, atributo);

            using var resultado = await database.FindAsync(filtro);

            return await resultado.FirstOrDefaultAsync();
        }

        public async Task DeletarAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.Guid, guid);
            await database.DeleteOneAsync(filtro, cancellationToken);
        }

        public async Task<PaginacaoConsulta<Usuario>> ListarAsync(CancellationToken cancellationToken = default)
        {
            var filtro = Builders<Usuario>.Filter.Empty;
            using var resultado = await database.FindAsync(filtro);

            var resultados = await resultado.ToListAsync();

            return new PaginacaoConsulta<Usuario>
            {
                Total = resultados.Count,
                Resultados = resultados
            };
        }

        public void RedisInserir(string chave, string valor)
        {
            this.redis.StringSet(chave, valor,TimeSpan.FromHours(2));
        }
        
        public async Task RedisDeletarAsync(string chave)
        {
            await this.redis.KeyDeleteAsync(chave);
        }
        
        public string RedisBuscar(string chave)
        {
            RedisValue resultado = this.redis.StringGet(chave);

            return resultado.ToString();
        }

    }
}
