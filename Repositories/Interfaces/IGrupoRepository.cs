namespace NotasAPI.Repositories.Interfaces;

public interface IGrupoRepository : IRepository<Grupo>
{
    public Task<GrupoDTO> CreateGrupoAsync(GrupoInsertDTO insertDTO);

    public Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync(int idGrupo);

    public Task<IEnumerable<RecordatorioDTO>> GetRecordatoriosAsync(int idGrupo);

    public Task<GrupoWithUserDTO> InsertToGrupoAsync(GrupoDTO grupo, UsuarioDTO usuario);

    public Task<bool> GrupoExists(int codigo);

    public Task<Grupo> GrupoExistsWithGrupo(int codigo);
}
