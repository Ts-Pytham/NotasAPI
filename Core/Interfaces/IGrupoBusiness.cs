namespace NotasAPI.Core.Interfaces;

public interface IGrupoBusiness
{
    public Task<Response<GrupoDTO>> CreateGrupoAsync(GrupoInsertDTO insertDTO);

    public Task<Response<IEnumerable<UsuarioDTO>>> GetUsuariosAsync(long idGrupo);

    public Task<Response<IEnumerable<RecordatorioDTO>>> GetRecordatoriosAsync(long idGrupo);

    public Task<Response<GrupoWithUserDTO>> InsertToGrupoAsync(int idCodigo, long idUsuario);

    public Task<Response<IEnumerable<GrupoDTO>>> GetAllGrupos();

    public Task<Response<IEnumerable<UsuarioDTO>>> AddUsersInGroup(long idGrupo, IEnumerable<UsuarioDTO> usuarios);

    public Task<Response<UsuarioDTO>> LeaveGroup(long idGrupo, long idUsuario);
    public Task<Response<IEnumerable<GrupoDTO>>> GetGroupsOfUsers(long idUsuario);
}
