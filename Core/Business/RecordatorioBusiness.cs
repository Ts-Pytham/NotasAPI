namespace NotasAPI.Core.Business
{
    public class RecordatorioBusiness : IRecordatorioBusiness
    {
        private readonly IRecordatorioRepository _recordatorioRepository;

        public async Task<Response<RecordatorioInsertDTO>> CreateRecordatorio(int idUsuario, RecordatorioInsertDTO insertDTO)
        {
            if(await _recordatorioRepository.GetAsync(x => x.IdUsuario == idUsuario) is null)
            {
                var errors = new string[]
                {
                    "El usuario no existe!",
                    "No se pudo crear un recordatorio"
                };
                return new Response<RecordatorioInsertDTO>(null, false, errors, ResponseMessage.NotFound);
            }

            await _recordatorioRepository.CreateRecordatorioAsync(idUsuario, insertDTO);

            return new Response<RecordatorioInsertDTO>(insertDTO);
        }

        public async Task<Response<IEnumerable<RecordatorioDTO>>> GetAllRecordatorios(int idUsuario)
        {
            var recordatorios = await _recordatorioRepository.GetAllRecordatoriosAsync(idUsuario);

            if (recordatorios.Any())
            {
                return new Response<IEnumerable<RecordatorioDTO>>(recordatorios);
            }

            var errors = new string[]
            {
                "No hay ningún recordatorio con este usuario"
            };

            return new Response<IEnumerable<RecordatorioDTO>>(null, false, errors, ResponseMessage.NotFound)
        }
    }
}
