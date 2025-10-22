using Wearo.Application.Interfaces;
using Wearo.Application.Services;

namespace WearoSkillbox.Host.DI;

internal static class ApplicationConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
