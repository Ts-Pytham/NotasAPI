namespace NotasAPI.Repositories.Interfaces
{
    public interface IRecordatorioRepository : IRepository<Recordatorio>
    {
        Task<RecordatorioInsertDTO> CreateRecordatorioAsync(int idUsuario, RecordatorioInsertDTO insertDTO);

        Task<RecordatorioWithGroupDTO> CreateRecordatorioInGroupAsync(int idUsuario, int idGrupo, RecordatorioInsertDTO insertDTO);

        Task<RecordatorioUpdateDTO> UpdateRecordatorioAsync(int idUsuario, RecordatorioUpdateDTO updateDTO);

        Task<RecordatorioDeleteDTO> DeleteRecordatorioAsync(int idUsuario, RecordatorioDeleteDTO deleteDTO);

        Task<IEnumerable<RecordatorioDTO>> GetAllRecordatoriosAsync(int idUsuario);

        Task<RecordatorioDTO> GetRecordatorioAsync(int idUsuario, Expression<Func<Recordatorio, bool>> predicate);
    }
}
