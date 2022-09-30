namespace NotasAPI.Core.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUsuarioRepository, UsuarioRepository>()
                .AddTransient<IRecordatorioBusiness, RecordatorioBusiness>();
        return services;
    }

    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddTransient<IUsuarioBusiness, UsuarioBusiness>()
                .AddTransient<IRecordatorioRepository, RecordatorioRepository>();
        return services;
    }
}
