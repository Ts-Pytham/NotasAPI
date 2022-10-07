namespace NotasAPI.Repositories.Interfaces;

public interface IRecordatorioRepository : IRepository<Recordatorio>
{
    Task<RecordatorioInsertDTO> CreateRecordatorioAsync(long idUsuario, RecordatorioInsertDTO insertDTO);

    Task<RecordatorioWithGroupDTO> CreateRecordatorioInGroupAsync(long idUsuario, GrupoDTO grupo, RecordatorioInsertDTO insertDTO);

    Task<RecordatorioUpdateDTO> UpdateRecordatorioAsync(RecordatorioUpdateDTO updateDTO);

    Task<RecordatorioDeleteDTO> DeleteRecordatorioAsync(long idRecordatorio);

    Task<IEnumerable<RecordatorioDTO>> GetAllRecordatoriosAsync(long idUsuario);

    Task<RecordatorioDTO> GetRecordatorioAsync(long idUsuario, Expression<Func<Recordatorio, bool>> predicate);
}
