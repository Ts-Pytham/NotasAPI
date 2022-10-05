namespace NotasAPI.Core.Business;

public class GrupoBusiness : IGrupoBusiness
{
    private readonly IGrupoRepository _grupoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public GrupoBusiness(IGrupoRepository grupoRepository, IUsuarioRepository usuarioRepository)
    {
        _grupoRepository = grupoRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Response<GrupoDTO>> CreateGrupoAsync(GrupoInsertDTO insertDTO)
    {
        try
        {

            if(! await _grupoRepository.CheckMonitor(insertDTO.IdMonitor))
            {
                var errors = new string[]
                {
                    "El estudiante no es un monitor!",
                    "No se pudo crear un grupo!"
                };

                return new Response<GrupoDTO>(null, false, errors, ResponseMessage.NotFound);
            }
            
            int code;
            while (true)
            {
                code = new Random().Next(int.MinValue, int.MaxValue);

                if (!await _grupoRepository.GrupoExists(code))
                {
                    break;
                }
            }

            var grupo = await _grupoRepository.CreateGrupoAsync(insertDTO, code);

            return new Response<GrupoDTO>(grupo);
        }
        catch(Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se pudo crear el grupo"
            };

            return new Response<GrupoDTO>(null, false, errors, ResponseMessage.UnexpectedErrors);
        }
    }

    public async Task<Response<IEnumerable<RecordatorioDTO>>> GetRecordatoriosAsync(int idGrupo)
    {
        try
        {
            if(await _grupoRepository.GetAsync(x => x.Id == idGrupo) is null)
            {
                var errors = new string[]
                {
                    "Este grupo no existe!",
                    "No se ha podido recuperar los recordatorios"
                };

                return new Response<IEnumerable<RecordatorioDTO>>(null, false, errors, ResponseMessage.NotFound);
            }

            var recordatorios = await _grupoRepository.GetRecordatoriosAsync(idGrupo);

            return new Response<IEnumerable<RecordatorioDTO>>(recordatorios);
        }
        catch(Exception e)
        {
            var errors = new string[]
            {
                    e.Message,
                    "No se ha podido recuperar los recordatorios"
            };

            return new Response<IEnumerable<RecordatorioDTO>>(null, false, errors, ResponseMessage.UnexpectedErrors);
        }
    }

    public async Task<Response<IEnumerable<UsuarioDTO>>> GetUsuariosAsync(int idGrupo)
    {
        try
        {
            if (await _grupoRepository.GetByIdAsync(idGrupo) is null)
            {
                var errors = new string[]
                {
                    "Este grupo no existe!",
                    "No se ha podido recuperar los usuarios"
                };

                return new Response<IEnumerable<UsuarioDTO>>(null, false, errors, ResponseMessage.NotFound);
            }

            var usuarios = await _grupoRepository.GetUsuariosAsync(idGrupo);

            return new Response<IEnumerable<UsuarioDTO>>(usuarios);
        }
        catch (Exception e)
        {
            var errors = new string[]
            {
                    e.Message,
                    "No se ha podido recuperar los usuarios"
            };

            return new Response<IEnumerable<UsuarioDTO>>(null, false, errors, ResponseMessage.UnexpectedErrors);
        }
    }

    public async Task<Response<GrupoWithUserDTO>> InsertToGrupoAsync(int idCodigo, long idUsuario)
    {
        try
        {
            var grupo = await _grupoRepository.GrupoExistsWithGrupo(idCodigo);
            var usuario = await _usuarioRepository.GetByIdAsync(idUsuario);
            if (grupo is null || usuario is null)
            {
                var errors = new string[]
                {
                    "No se ha podido recuperar los usuarios",
                };

                if(grupo is null)
                {
                    errors = errors.Append("Este grupo no existe!").ToArray();
                }

                if (usuario is null)
                {
                    errors = errors.Append("Este usuario no existe!").ToArray();
                }


                return new Response<GrupoWithUserDTO>(null, false, errors, ResponseMessage.NotFound);
            }

            var g = await _grupoRepository.InsertToGrupoAsync(grupo.MapToGrupoDTO(), usuario);

            return new Response<GrupoWithUserDTO>(g);
        }
        catch(Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se ha podido ingresar al usuario al grupo!"
            };
          
            return new Response<GrupoWithUserDTO>(null, false, errors, ResponseMessage.UnexpectedErrors);
        }
    }
}
