namespace NotasAPI.Repositories.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task CreateUsuarioAsync(UsuarioInsertDTO usuario);
}
