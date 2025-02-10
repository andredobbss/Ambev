using Ambev.Application.Interfaces;
using Ambev.Application.Services;
using Ambev.Application.Validation;
using Ambev.Domain.Entities;
using Ambev.Domain.IUnitOfWork;
using Ambev.Domain.Repositories;
using Ambev.Domain.Repositories.Auth;
using Ambev.Domain.Repositories.Security;
using Ambev.Infraestructure.Database;
using Ambev.Infraestructure.Database.MongoConfigurations;
using Ambev.Infraestructure.Repositories;
using Ambev.Infraestructure.Repositories.Auth;
using Ambev.Infraestructure.Repositories.Filter;
using Ambev.Infraestructure.Repositories.Security;
using Ambev.Infraestructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ambev.CrossCutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(
                                      this IServiceCollection services,
                                      IConfiguration configuration)
    {
        #region Configura Conexão Postgres

        string? postgresConnection = configuration.GetConnectionString("PostgresConnection");

        services.AddDbContext<AppDbContext>(options =>
           options.UseNpgsql(postgresConnection), ServiceLifetime.Scoped);

        #endregion

        #region Confirura Conexão MongoDB


        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });


        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase("mydatabase");
        });

        #endregion

        #region Registra Injeções

      
        services.AddScoped<IMongoCollection<UserDomain>>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var database = client.GetDatabase(settings.DatabaseName);
            return database.GetCollection<UserDomain>("Users"); // Nome da collection
        });

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return client.GetDatabase(settings.DatabaseName);
        });


        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped(typeof(MongoFilterService<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion

        #region Registra MediatR

        var myHandlers = AppDomain.CurrentDomain.Load("Ambev.Application");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(myHandlers);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        #endregion

        #region Retorno

        return services;

        #endregion 
    }

    public static DbContext Context(this DbContext context, IServiceScope scope)
    {
        return scope.ServiceProvider.GetRequiredService<AppDbContext>();

    }
}
