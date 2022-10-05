namespace NotasAPI.Repositories;

public class GrupoRepository : Repository<Grupo>, IGrupoRepository
{
    public GrupoRepository(NotesContext context) : base(context)
    {
    }

    public async Task<GrupoDTO> CreateGrupoAsync(GrupoInsertDTO insertDTO, int codigo)
    {
        var grupo = insertDTO.MapToGrupo(codigo);
        Context.Add(grupo);

        await SaveAsync();

        return grupo.MapToGrupoDTO();
    }

    public async Task<IEnumerable<RecordatorioDTO>> GetRecordatoriosAsync(int idGrupo)
    {
        var recordatoriosList = await Context.Set<GrupoConRecordatorio>()
                                             .Include(x => x.IdRecordatorioNavigation)
                                             .Where(x => x.Id == idGrupo)
                                             .Select(x => x.IdRecordatorioNavigation.MapToRecordatorioDTO())
                                             .ToListAsync();

        return recordatoriosList;
    }

    public async Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync(int idGrupo)
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

        Context.Add(grupoWithUser);

        await SaveAsync();

        return usuarioDto.MapToGrupoWithUserDTO(grupo);
    }


    public async Task<bool> CheckMonitor(long idMonitor)
    {
        return await Context.Set<Usuario>()
                            .Include(x => x.IdRolNavigation)
                            .Where(x => x.Id == idMonitor && x.IdRolNavigation.Id == (int)RolEnum.Monitor)
                            .AnyAsync();
    }
}
