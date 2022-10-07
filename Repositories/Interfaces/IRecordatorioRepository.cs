namespace NotasAPI.Repositories.Interfaces;

public interface IRecordatorioRepository : IRepository<Recordatorio>
{
    Task<RecordatorioInsertDTO> CreateRecordatorioAsync(int idUsuario, RecordatorioInsertDTO insertDTO);

    Task<RecordatorioWithGroupDTO> CreateRecordatorioInGroupAsync(int idUsuario, GrupoDTO grupo, RecordatorioInsertDTO insertDTO);

    Task<RecordatorioUpdateDTO> UpdateRecordatorioAsync(RecordatorioUpdateDTO updateDTO);

    Task<RecordatorioDeleteDTO> DeleteRecordatorioAsync(long idRecordatorio);

    Task<IEnumerable<RecordatorioDTO>> GetAllRecordatoriosAsync(int idUsuario);

    Task<RecordatorioDTO> GetRecordatorioAsync(int idUsuario, Expression<Func<Recordatorio, bool>> predicate);
}
