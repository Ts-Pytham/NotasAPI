namespace NotasAPI.Repositories;

public class GrupoRepository : Repository<Grupo>, IGrupoRepository
{
    public GrupoRepository(NotesContext context) : base(context)
    {
    }

    public async Task<GrupoDTO> CreateGrupoAsync(GrupoInsertDTO insertDTO, int codigo)
    {
        var grupo = insertDTO.MapToGrupo(codigo);

        await Context.AddAsync(grupo);

        var monitor = await Context.Set<Usuario>()
                                   .Where(x => x.Id == insertDTO.IdMonitor)
                                   .FirstOrDefaultAsync();

        await Context.SaveChangesAsync();
        var grupoDTO = grupo.MapToGrupoDTO();
        await InsertToGrupoAsync(grupoDTO, monitor);

        return grupoDTO;
    }

    public async Task<IEnumerable<RecordatorioDTO>> GetRecordatoriosAsync(long idGrupo)
    {
        var recordatoriosList = await Context.Set<GrupoConRecordatorio>()
                                             .Include(x => x.IdRecordatorioNavigation)
                                             .Where(x => x.IdGrupo == idGrupo)
                                             .Select(x => x.IdRecordatorioNavigation.MapToRecordatorioDTO())
                                             .ToListAsync();

        return recordatoriosList;
    }

    public async Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync(long idGrupo)
    {
        var usuariosList = await Context.Set<GrupoConUsuario>()
                                        .Include(x => x.IdUsuarioNavigation)
                                        .Where(x => x.IdGrupo == idGrupo)
                                        .Select(x => x.IdUsuarioNavigation.MapToUsuarioDTO())
                                        .ToListAsync();

        return usuariosList;
    }

    public async Task<bool> GrupoExists(int codigo)
    {
        var exists = await Context.Set<Grupo>()
                                  .Where(x => x.Codigo == codigo)
                                  .AnyAsync();

        return exists;
    }

    public async Task<Grupo> GrupoExistsWithGrupo(int codigo)
    {
        var exists = await Context.Set<Grupo>()
                                  .Where(x => x.Codigo == codigo)
                                  .FirstOrDefaultAsync();

        return exists;
    }

    public async Task<GrupoWithUserDTO> InsertToGrupoAsync(GrupoDTO grupo, Usuario usuario)
    {
        var usuarioDto = usuario.MapToUsuarioDTO();
        var grupoWithUser = usuarioDto.MapToGrupoConUsuario(grupo.Id);

        await Context.AddAsync(grupoWithUser);

        await SaveAsync();

        return usuarioDto.MapToGrupoWithUserDTO(grupo);
    }

    public async Task AddUsersInGroupAsync(long idGrupo, IEnumerable<UsuarioDTO> usuarios)
    {
        var grupoWithUsers = usuarios.Select(x => x.MapToGrupoConUsuario(idGrupo));

        await Context.AddRangeAsync(grupoWithUsers);

        await SaveAsync();
    }

    public async Task<bool> UserExistsAndUserInGroup(long idUsuario, long idGrupo)
    {
        return await Context.Set<GrupoConUsuario>()
                            .Where(x => x.Id == idUsuario && x.IdGrupo == idGrupo)
                            .AnyAsync();
    }
}
