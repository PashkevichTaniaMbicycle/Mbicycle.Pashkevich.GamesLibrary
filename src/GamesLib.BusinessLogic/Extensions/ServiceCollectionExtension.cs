using Microsoft.Extensions.DependencyInjection;
using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddSingleton(new GamesLibContext());
            services.AddScoped<IDevRepository, DevRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
        }
    }
}