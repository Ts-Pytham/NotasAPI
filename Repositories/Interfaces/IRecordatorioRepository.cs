namespace NotasAPI.Repositories.Interfaces;

public interface IRecordatorioRepository : IRepository<Recordatorio>
{
    Task<RecordatorioDTO> CreateRecordatorioAsync(long idUsuario, RecordatorioInsertDTO insertDTO);

    Task<IEnumerable<RecordatorioWithGroupDTO>> CreateRecordatorioInGroupsAsync(RecordatorioWithGroupsInsertDTO insert);

    Task<RecordatorioUpdateDTO> UpdateRecordatorioAsync(RecordatorioUpdateDTO updateDTO);

    Task<RecordatorioDeleteDTO> DeleteRecordatorioAsync(long idRecordatorio);

    Task<IEnumerable<RecordatorioDTO>> GetAllRecordatoriosAsync(long idUsuario);

    Task<RecordatorioDTO> GetRecordatorioAsync(long idUsuario, Expression<Func<Recordatorio, bool>> predicate);
}
