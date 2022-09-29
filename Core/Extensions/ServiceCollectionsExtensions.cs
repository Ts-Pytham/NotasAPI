namespace NotasAPI.Core.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUsuarioRepository, UsuarioRepository>()
                ;
        return services;
    }

    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddTransient<IUsuarioBusiness, UsuarioBusiness>();
        return services;
    }
}
