namespace NotasAPI.Core.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUsuarioRepository, UsuarioRepository>()
                .AddTransient<IRecordatorioRepository, RecordatorioRepository>()
                .AddTransient<IGrupoRepository, GrupoRepository>();
        return services;
    }

    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddTransient<IUsuarioBusiness, UsuarioBusiness>()
                .AddTransient<IRecordatorioBusiness, RecordatorioBusiness>()
                .AddTransient<IGrupoBusiness, GrupoBusiness>();
        return services;
    }
}
