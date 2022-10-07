namespace NotasAPI.Core.Business;

public class RecordatorioBusiness : IRecordatorioBusiness
{
    private readonly IRecordatorioRepository _recordatorioRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IGrupoRepository _grupoRepository;

    public RecordatorioBusiness(IRecordatorioRepository recordatorioRepository, IUsuarioRepository usuarioRepository, IGrupoRepository grupoRepository)
    {
        _recordatorioRepository = recordatorioRepository;
        _usuarioRepository = usuarioRepository;
        _grupoRepository = grupoRepository;
    }

    public async Task<Response<RecordatorioInsertDTO>> CreateRecordatorio(int idUsuario, RecordatorioInsertDTO insertDTO)
    {
        var result  = await _recordatorioRepository.CreateRecordatorioAsync(idUsuario, insertDTO);

        if (result is null)
        {
            var errors = new string[]
            {
                "El usuario no existe!",
                "No se pudo crear un recordatorio"
            };
            return new Response<RecordatorioInsertDTO>(null, false, errors, ResponseMessage.NotFound);
        }

        return new Response<RecordatorioInsertDTO>(insertDTO);
    }

    public async Task<Response<RecordatorioWithGroupDTO>> CreateRecordatorioInGroup(int idUsuario, int idGrupo, RecordatorioInsertDTO insertDTO)
    {
        try
        {
            var existsUser = await _usuarioRepository.GetByIdAsync(idUsuario);
            var existsGroup = await _grupoRepository.GetByIdAsync(idGrupo);

            if (existsGroup is null || existsUser is null)
            {
                var errors = new string[]
                {
                    "No se pudo crear un recordatorio"
                };

                if (existsGroup is null)
                {
                    errors = errors.Append("El grupo no existe!").ToArray();
                }

                if (existsUser is null)
                {
                    errors = errors.Append("El usuario no existe!").ToArray();
                }

                return new Response<RecordatorioWithGroupDTO>(null, false, errors, ResponseMessage.NotFound);
            }

            var gr = await _recordatorioRepository.CreateRecordatorioInGroupAsync(idUsuario, existsGroup.MapToGrupoDTO(), insertDTO);

            return new Response<RecordatorioWithGroupDTO>(gr);
        }
        catch(Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se pudo crear un recordatorio"
            };

            return new Response<RecordatorioWithGroupDTO>(null, false, errors, ResponseMessage.UnexpectedErrors);
        }
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

        return new Response<IEnumerable<RecordatorioDTO>>(null, false, errors, ResponseMessage.NotFound);
    }
}
