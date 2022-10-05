namespace NotasAPI.Core.Interfaces;

public interface IGrupoBusiness
{
    public Task<Response<GrupoDTO>> CreateGrupoAsync(GrupoInsertDTO insertDTO);

    public Task<Response<IEnumerable<UsuarioDTO>>> GetUsuariosAsync(int idGrupo);

    public Task<Response<IEnumerable<RecordatorioDTO>>> GetRecordatoriosAsync(int idGrupo);

    public Task<Response<GrupoWithUserDTO>> InsertToGrupoAsync(int idCodigo, UsuarioDTO usuario);
}
