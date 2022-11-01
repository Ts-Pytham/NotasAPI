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

    public async Task<Response<IEnumerable<RecordatorioWithGroupDTO>>> CreateRecordatorioInGroups(RecordatorioWithGroupsInsertDTO insert)
    {
        try
        {
            for (int i = 0; i != insert.IdGrupos.Length; ++i)
            {
                var existsMonitor = await _grupoRepository.CheckMonitorInGroup(insert.IdMonitor, insert.IdGrupos[i]);


                string[] errors = Array.Empty<string>();

                errors = existsMonitor switch
                {
                    1 => errors.Append("El monitor no pertecene al grupo!").ToArray(),
                    2 => errors.Append("El usuario no es monitor!").ToArray(),
                    3 => errors.Append("El grupo no existe!").ToArray(),
                    4 => errors.Append("No existe ni el grupo y el monitor!").ToArray(),
                    0 => default,
                    _ => throw new NotImplementedException()
                };
                if (existsMonitor != 0)
                {
                    return new Response<IEnumerable<RecordatorioWithGroupDTO>>(null, false, errors, ResponseMessage.NotFound);
                }
            }

            var gr = await _recordatorioRepository.CreateRecordatorioInGroupsAsync(insert);

            return new Response<IEnumerable<RecordatorioWithGroupDTO>>(gr);
        }
        catch(Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se pudo crear un recordatorio"
            };

            return new Response<IEnumerable<RecordatorioWithGroupDTO>>(null, false, errors, ResponseMessage.UnexpectedErrors);
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
        
        return new Response<IEnumerable<RecordatorioDTO>>(recordatorios);
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
