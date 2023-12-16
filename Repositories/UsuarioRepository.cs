namespace NotasAPI.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(NotesContext context) : base(context)
    {
    }

    public async Task<UsuarioDTO> CreateUsuarioAsync(UsuarioInsertDTO usuarioDTO)
    {
        var usuario = usuarioDTO.MapToUsuario();

        await InsertAsync(usuario);
        await SaveAsync();

        return usuario.MapToUsuarioDTO();
    }

    public async Task<UsuarioDTO> LoginUsuarioAsync(UsuarioLoginDTO usuarioDTO)
    {

        var existsUser = await Context.Set<Usuario>()
                                          .Where(x => x.Correo == usuarioDTO.Name || x.Codigo == usuarioDTO.Name.ToInt32())
                                          .FirstOrDefaultAsync();


        if (existsUser is null)
            return null;

        if (!usuarioDTO.Password.VerifyHashPasswordBCrypt(existsUser.Contraseña))
            return null;

        return existsUser.MapToUsuarioWithRolDTO();
    }

    public async Task<bool> CheckMonitor(long idMonitor)
    {
        return await Context.Set<Usuario>()
                            .Include(x => x.IdRolNavigation)
                            .Where(x => x.Id == idMonitor && x.IdRolNavigation.Id == (int)RolEnum.Monitor)
                            .AnyAsync();
    }
}
