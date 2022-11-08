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
                                             .Include(x => x.IdRecordatorioNavigation.IdUsuarioNavigation)
                                             .Where(x => x.IdGrupo == idGrupo)
                                             .Select(x => x.IdRecordatorioNavigation.MapToRecordatorioDTO($"{x.IdRecordatorioNavigation.IdUsuarioNavigation.Nombre} (Monitor)"))
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

    public async Task<bool> UserExistsInGroup(long idUsuario, long idGrupo)
    {
        return await Context.Set<GrupoConUsuario>()
                            .Where(x => x.IdUsuario == idUsuario && x.IdGrupo == idGrupo)
                            .AnyAsync();
    }

    public async Task<GrupoConUsuario> GetGroupWithUser(long idGrupo, long idUsuario)
    {
        return await Context.Set<GrupoConUsuario>()
                            .Where(x => x.IdUsuario == idUsuario && x.IdGrupo == idGrupo)
                            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<GrupoDTO>> GetGroupsOfUsers(long idUsuario)
    {
        return await Context.Set<GrupoConUsuario>()
                            .Include(x => x.IdGrupoNavigation)
                            .Where(x => x.IdUsuario == idUsuario)
                            .Select(x => x.IdGrupoNavigation.MapToGrupoDTO())
                            .ToListAsync();
    }

    /// <summary>
    /// Comprueba si el usuario es monitor de ese grupo.
    /// </summary>
    /// <param name="idMonitor"></param>
    /// <param name="idGrupo"></param>
    /// <returns>un entero:
    ///                 0 Si el usuario es monitor de ese grupo.
    ///                 1 Si el monitor no pertecene al grupo.
    ///                 2 Si el usuario no es monitor.
    ///                 3 Si el grupo no existe.
    ///                 4 Si no existe ni el grupo ni el monitor.</returns>
    public async Task<int> CheckMonitorInGroup(long idMonitor, long idGrupo)
    {
        var monitor = await Context.Set<Usuario>()
                            .Include(x => x.IdRolNavigation)
                            .Where(x => x.Id == idMonitor && x.IdRolNavigation.Id == (int)RolEnum.Monitor)
                            .AnyAsync();

        var grupo = await Context.Set<Grupo>()
                                 .Where(x => idGrupo == x.Id)
                                 .AnyAsync();

        if (!grupo && !monitor)
            return 4;

        if (!grupo)
            return 3;

        if (!monitor)
            return 2;

        var result = await Context.Set<Grupo>()
                                  .Where(x => x.Id == idGrupo && idMonitor == x.IdMonitor)
                                  .AnyAsync();

        if (!result)
            return 1;

        return 0;
                             
    }

    public async Task LeaveGroup(GrupoConUsuario usuario)
    {
        Context.Remove(usuario);
        await SaveAsync();
    }

}
