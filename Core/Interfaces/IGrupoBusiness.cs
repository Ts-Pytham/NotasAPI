namespace NotasAPI.Core.Interfaces;

public interface IGrupoBusiness
{
    public Response<Task<GrupoDTO>> CreateGrupoAsync(GrupoInsertDTO insertDTO);

    public Response<Task<IEnumerable<UsuarioDTO>>> GetUsuariosAsync(int idGrupo);

    public Response<Task<IEnumerable<RecordatorioDTO>>> GetRecordatoriosAsync(int idGrupo);

    public Response<Task<GrupoWithUserDTO>> InsertToGrupoAsync(int idCodigo, UsuarioDTO usuario);
}
