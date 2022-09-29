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
                                          .Include(x => x.IdRolNavigation)
                                          .Where(x => usuarioDTO.Password.VerifyHashPasswordBCrypt(x.Contraseña) && x.Correo == usuarioDTO.Name || x.Codigo == usuarioDTO.Name.ToInt32())
                                          .FirstOrDefaultAsync();

        
        return existsUser?.MapToUsuarioWithRolDTO();
    }
}
