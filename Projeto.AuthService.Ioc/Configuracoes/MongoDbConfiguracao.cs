using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.AutService.Ioc.Configuracoes
{
    internal static class MongoDbConfiguracao
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(configuration.GetConnectionString("MongoDb")));

            services.AddScoped(sp =>
                sp.GetRequiredService<IMongoClient>().GetDatabase(configuration.GetSection("MongoDb.Database").Value));
        }
    }
}
