namespace NotasAPI.Repositories.Interfaces;

public interface IGrupoRepository : IRepository<Grupo>
{
    public Task<GrupoDTO> CreateGrupoAsync(GrupoInsertDTO insertDTO, int codigo);

    public Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync(long idGrupo);

    public Task<IEnumerable<RecordatorioDTO>> GetRecordatoriosAsync(long idGrupo);

    public Task<GrupoWithUserDTO> InsertToGrupoAsync(GrupoDTO grupo, Usuario usuario);

    public Task<bool> GrupoExists(int codigo);

    public Task<Grupo> GrupoExistsWithGrupo(int codigo);

}
