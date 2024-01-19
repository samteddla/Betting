using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SportBet.Infrastructure.Repositories.Configurations;
using System.Data;

namespace SportBet.Infrastructure;
public static class RepositoryExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var defaultConnections = new ConnectionStrings();
        configuration.Bind(nameof(ConnectionStrings), defaultConnections);
        services.AddSingleton(Options.Create(defaultConnections));

        services.AddDbContext<BetContext>(options => options.UseSqlServer(defaultConnections.DefaultConnection));
        services.AddScoped<IDbConnection>(provider => new SqlConnection(defaultConnections.DefaultConnection));

        return services;
    }
}
