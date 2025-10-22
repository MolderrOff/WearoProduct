namespace WearoSkillbox.Host.DI;

public static class ModuleConfigurator
{
    public static IServiceCollection ConfigureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureApplicationServices(configuration);
        services.ConfigureInfrastructureServices(configuration);

        return services;
    }
}
