using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Wearo.Domain.Interfaces;
using Wearo.Infrastructure.Persistens;
using Wearo.Infrastructure.Persistens.Repositories;

namespace WearoSkillbox.Host.DI;

internal static class InfrastructureConfigurator
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString,
                x => x.MigrationsHistoryTable("__EFMigrationsHistory"));
        });

        ConfigureRepositories(services, configuration);
    }
    public static void ConfigureRepositories(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
