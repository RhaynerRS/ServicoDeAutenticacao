using Projeto.AuthService.Aplicacao.Usuarios.Servicos;
using Projeto.AuthService.DataTransfer.Usuarios.Request;
using Projeto.AuthService.Dominio.Usuarios.Servicos;
using Projeto.AuthService.Infra.Usuarios;
using Projeto.AuthService.IOC.Configuracoes.Redis;
using Projeto.AutService.Ioc.Configuracoes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace Projeto.AuthService.IOC
{
    public static class NativeInjectorBoostraper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRedis(configuration);

            services.AddMongoDb(configuration);

            services.AddHttpContextAccessor();

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            services.AddServicesFromAssembly(typeof(UsuarioAppServico).Assembly);
            services.AddServicesFromAssembly(typeof(UsuarioServico).Assembly);
            services.AddServicesFromAssembly(typeof(UsuarioRepositorio).Assembly);
        }
    }
}
