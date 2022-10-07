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

        public async Task<RecordatorioWithGroupDTO> CreateRecordatorioInGroupAsync(int idUsuario, GrupoDTO grupo, RecordatorioInsertDTO insertDTO)
        {
            var recordatorio = insertDTO.MapToRecordatorio(idUsuario);

            await Context.AddAsync(recordatorio);

            var gr = recordatorio.MapToGrupoConRecordatorioDTO(grupo.Id);

            await Context.AddAsync(gr);

            await SaveAsync();

            return gr.MapToRecordatorioWithGroupDTO(grupo, recordatorio.MapToRecordatorioDTO());

        }


        public async Task<RecordatorioDeleteDTO> DeleteRecordatorioAsync(long idRecordatorio)
        {
            var recordatorio = await Context.Set<Recordatorio>()
                                            .Where(x => x.Id == idRecordatorio)
                                            .FirstOrDefaultAsync();

            
            if (recordatorio is null)
            {
                return null;
            }

            var deleteDTO = recordatorio.MapToRecordatorioDeleteDTO();

            Context.Remove(recordatorio);

            await SaveAsync();

            return deleteDTO;
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

        public async Task<RecordatorioUpdateDTO> UpdateRecordatorioAsync(RecordatorioUpdateDTO updateDTO)
        {
            var recordatorio = await Context.Set<Recordatorio>()
                                            .Where(x => x.Id == updateDTO.Id)
                                            .FirstOrDefaultAsync();

            if(recordatorio is null)
            {
                return null;
            }

            recordatorio.SetToRecordatorio(updateDTO);

            await SaveAsync();

            return updateDTO;
        }
    }
}
