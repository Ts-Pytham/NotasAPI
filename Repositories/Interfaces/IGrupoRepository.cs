﻿namespace NotasAPI.Repositories.Interfaces;

public interface IGrupoRepository : IRepository<Grupo>
{
    public Task<GrupoDTO> CreateGrupoAsync(GrupoInsertDTO insertDTO, int codigo);

    public Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync(long idGrupo);

    public Task<IEnumerable<RecordatorioDTO>> GetRecordatoriosAsync(long idGrupo);

    public Task<GrupoWithUserDTO> InsertToGrupoAsync(GrupoDTO grupo, Usuario usuario);

    public Task<bool> GrupoExists(int codigo);

    public Task<Grupo> GrupoExistsWithGrupo(int codigo);

    public Task AddUsersInGroupAsync(long idGrupo, IEnumerable<UsuarioDTO> usuarios);

    public Task<bool> UserExistsAndUserInGroup(long idUsuario, long idGrupo);

    public Task<IEnumerable<GrupoDTO>> GetGroupsOfUsers(long idUsuario);

    public Task<int> CheckMonitorInGroup(long idMonitor, long idGrupo);
}
