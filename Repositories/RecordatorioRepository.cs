namespace NotasAPI.Repositories
{
    public class RecordatorioRepository : Repository<Recordatorio>, IRecordatorioRepository
    {
        public RecordatorioRepository(NotesContext context) : base(context)
        {
        }

        public async Task<RecordatorioInsertDTO> CreateRecordatorioAsync(int idUsuario, RecordatorioInsertDTO insertDTO)
        {
            var existsUser = await Context.Set<Usuario>().Where(x => x.Id == idUsuario).AnyAsync();

            if (!existsUser)
                return null;

            var recordatorio = insertDTO.MapToRecordatorio(idUsuario);

            await Context.AddAsync(recordatorio);

            await SaveAsync();

            return insertDTO;
        }

        public Task<RecordatorioDeleteDTO> DeleteRecordatorioAsync(int idUsuario, RecordatorioDeleteDTO deleteDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RecordatorioDTO>> GetAllRecordatoriosAsync(int idUsuario)
        {
            var recordatorios = (await GetEntitiesAsync(x => x.IdUsuario == idUsuario))
                                .Select(x => x.MapToRecordatorioDTO());

            return recordatorios;
        }

        public Task<RecordatorioDTO> GetRecordatorioAsync(int idUsuario, Expression<Func<Recordatorio, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<RecordatorioUpdateDTO> UpdateRecordatorioAsync(int idUsuario, RecordatorioUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
