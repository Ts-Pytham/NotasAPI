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

    public async Task<Response<RecordatorioDTO>> CreateRecordatorio(long idUsuario, RecordatorioInsertDTO insertDTO)
    {
        var result  = await _recordatorioRepository.CreateRecordatorioAsync(idUsuario, insertDTO);

        if (result is null)
        {
            var errors = new string[]
            {
                "El usuario no existe!",
                "No se pudo crear un recordatorio"
            };
            return new Response<RecordatorioDTO>(null, false, errors, ResponseMessage.NotFound);
        }

        return new Response<RecordatorioDTO>(result);
    }

    public async Task<Response<RecordatorioWithGroupDTO>> CreateRecordatorioInGroup(long idUsuario, long idGrupo, RecordatorioInsertDTO insertDTO)
    {
        try
        {
            var existsMonitor = await _usuarioRepository.CheckMonitor(idUsuario);
            var existsGroup = await _grupoRepository.GetByIdAsync(idGrupo);

            if (existsGroup is null || !existsMonitor)
            {
                var errors = new string[]
                {
                    "No se pudo crear un recordatorio"
                };

                if (existsGroup is null)
                {
                    errors = errors.Append("El grupo no existe!").ToArray();
                }

                if (!existsMonitor)
                {
                    errors = errors.Append("Este monitor no existe!").ToArray();
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

    public async Task<Response<RecordatorioDeleteDTO>> DeleteRecordatorio(long idRecordatorio)
    {
        try
        {
            var recordatorio = await _recordatorioRepository.DeleteRecordatorioAsync(idRecordatorio);

            if (recordatorio is null)
            {
                var errors = new string[]
                {
                    "El id del recordatorio no se encontró!",
                    "No se pudo eliminar el recordatorio!"
                };

                return new Response<RecordatorioDeleteDTO>(null, false, errors, ResponseMessage.NotFound);
            }

            return new Response<RecordatorioDeleteDTO>(recordatorio);
        }
        catch (Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se pudo eliminar el recordatorio!"
            };

            return new Response<RecordatorioDeleteDTO>(null, false, errors, ResponseMessage.NotFound);
        }
    }

    public async Task<Response<IEnumerable<RecordatorioDTO>>> GetAllRecordatorios(long idUsuario)
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

    public async Task<Response<RecordatorioUpdateDTO>> UpdateRecordatorio(RecordatorioUpdateDTO updateDTO)
    {
        try
        {
            var recordatorio = await _recordatorioRepository.UpdateRecordatorioAsync(updateDTO);

            if (recordatorio is null)
            {
                var errors = new string[]
                {
                    "El id del recordatorio no se encontró!",
                    "No se pudo actualizar el recordatorio!"
                };

                return new Response<RecordatorioUpdateDTO>(null, false, errors, ResponseMessage.NotFound);
            }

            return new Response<RecordatorioUpdateDTO>(recordatorio);
        }
        catch(Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se pudo actualizar el recordatorio!"
            };

            return new Response<RecordatorioUpdateDTO>(null, false, errors, ResponseMessage.NotFound);
        }
        
    }
}
