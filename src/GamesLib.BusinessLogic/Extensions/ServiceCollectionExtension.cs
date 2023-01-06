using FluentValidation;
using GamesLib.BusinessLogic.Handlers.Commands.Dev;
using GamesLib.BusinessLogic.Handlers.Commands.Game;
using GamesLib.BusinessLogic.Handlers.Commands.Publisher;
using Microsoft.Extensions.DependencyInjection;
using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GamesLib.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            //context
            var context = new GamesLibContext();
            services.AddSingleton(context);
            services.AddSingleton<DbContext>(context);
            
            //repositories
            services.AddScoped<IDevRepository, DevRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            
            //validators
            services.AddScoped<IValidator<AddGameCommand>, AddGameCommandValidator>();
            services.AddScoped<IValidator<UpdateGameCommand>, UpdateGameCommandValidator>();
            services.AddScoped<IValidator<DeleteGameCommand>, DeleteGameCommandValidator>();
            
            services.AddScoped<IValidator<AddDevCommand>, AddDevCommandValidator>();
            services.AddScoped<IValidator<UpdateDevCommand>, UpdateDevCommandValidator>();
            services.AddScoped<IValidator<DeleteDevCommand>, DeleteDevCommandValidator>();
            
            services.AddScoped<IValidator<AddPublisherCommand>, AddPublisherCommandValidator>();
            services.AddScoped<IValidator<UpdatePublisherCommand>, UpdatePublisherCommandValidator>();
            services.AddScoped<IValidator<DeletePublisherCommand>, DeletePublisherCommandValidator>();
        }
    }
}