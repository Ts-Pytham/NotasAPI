using NotasAPI.Entities;

namespace NotasAPI.Repositories
{
    public class RecordatorioRepository : Repository<Recordatorio>, IRecordatorioRepository
    {
        public RecordatorioRepository(NotesContext context) : base(context)
        {
        }

        public async Task<RecordatorioDTO> CreateRecordatorioAsync(long idUsuario, RecordatorioInsertDTO insertDTO)
        {
            var existsUser = await Context.Set<Usuario>().Where(x => x.Id == idUsuario).FirstOrDefaultAsync();

            if (existsUser is null)
                return null;

            var recordatorio = insertDTO.MapToRecordatorio(idUsuario);

            await Context.AddAsync(recordatorio);

            await SaveAsync();

            return recordatorio.MapToRecordatorioDTO($"{existsUser.Nombre} (Estudiante)");
        }

        public async Task<IEnumerable<RecordatorioWithGroupDTO>> CreateRecordatorioInGroupsAsync(RecordatorioWithGroupsInsertDTO insert)
        {
            long idMonitor = insert.IdMonitor;

            var list = new List<RecordatorioWithGroupDTO>();
            var name = await Context.Set<Usuario>()
                                    .Where(x => x.Id == idMonitor)
                                    .Select(x => x.Nombre)
                                    .FirstOrDefaultAsync();

            var recordatorio = insert.Recordatorio.MapToRecordatorio(idMonitor);

            await Context.AddAsync(recordatorio);

            foreach (var idGrupo in insert.IdGrupos)
            {
                var gr = recordatorio.MapToGrupoConRecordatorioDTO(idGrupo);

                await Context.AddAsync(gr);

                await SaveAsync();

                list.Add(gr.MapToRecordatorioWithGroupDTO(idGrupo, recordatorio.MapToRecordatorioDTO($"{name} (Monitor)")));
            }

            

            return list;

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

        public async Task<IEnumerable<RecordatorioDTO>> GetAllRecordatoriosAsync(long idUsuario)
        {
            /*
            var recordatorios = await Context.Set<Recordatorio>()
                                             .Include(x => x.IdUsuarioNavigation)
                                             .Include(x => x.IdUsuarioNavigation.IdRolNavigation)
                                             .Where(x => x.IdUsuario == idUsuario)
                                             .Select(x => x.MapToRecordatorioDTO($"{x.IdUsuarioNavigation.Nombre} ({x.IdUsuarioNavigation.IdRolNavigation.Nombre})"))
                                             .ToListAsync();

            */

            var recordatorios1 = await (from recordatorio in Context.Set<Recordatorio>()
                                        join usuario in Context.Set<Usuario>() on recordatorio.IdUsuario equals usuario.Id
                                        join rol in Context.Set<Rol>() on usuario.IdRol equals rol.Id
                                        orderby recordatorio.Id
                                        where recordatorio.IdUsuario == idUsuario
                                        select recordatorio.MapToRecordatorioDTO($"{usuario.Nombre} ({rol.Nombre})")).ToListAsync();

            var recordatorios2 = await  (from recordatorio in Context.Set<Recordatorio>()
                                         join GR in Context.Set<GrupoConRecordatorio>() on recordatorio.Id equals GR.IdRecordatorio
                                         join GU in Context.Set<GrupoConUsuario>() on GR.IdGrupo equals GU.IdGrupo
                                         join usuario in Context.Set<Usuario>() on GU.IdUsuario equals usuario.Id
                                         join rol in Context.Set<Rol>() on usuario.IdRol equals rol.Id
                                         orderby recordatorio.Id
                                         where GU.IdUsuario == idUsuario
                                         select recordatorio.MapToRecordatorioDTO($"{recordatorio.IdUsuarioNavigation.Nombre} ({recordatorio.IdUsuarioNavigation.IdRolNavigation.Nombre})")).ToListAsync();

            var recordatorios = recordatorios1.Union(recordatorios2).OrderBy(x => x.Id).DistinctBy(x => x.Id);

            return recordatorios;
        }

        public Task<RecordatorioDTO> GetRecordatorioAsync(long idUsuario, Expression<Func<Recordatorio, bool>> predicate)
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
