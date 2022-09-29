namespace NotasAPI.Core.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>()
                ;
        return services;
    }

    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        
        return services;
    }
}
