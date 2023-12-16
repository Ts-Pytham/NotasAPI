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

    public async Task<Response<IEnumerable<UsuarioDTO>>> AddUsersInGroup(long idGrupo, IEnumerable<UsuarioDTO> usuarios)
    {
        try
        {
            var existsGrupo = await _grupoRepository.GetByIdAsync(idGrupo);

            if (existsGrupo is null)
            {
                return new Response<IEnumerable<UsuarioDTO>>(null, false, new string[] { "No existe el grupo!" }, ResponseMessage.NotFound);
            }
            foreach (var usuario in usuarios)
            {
                if (await _grupoRepository.UserExistsInGroup(usuario.Id, idGrupo))
                {
                    var errors = new string[]
                    {
                        $"El usuario con ID {usuario.Id} ya se encuentra en el grupo o no está creado!",
                        $"No se ha podido ingresar {(usuarios.Count() > 1 ? "a los usuarios" : "al usuario")}!"
                    };
                    return new Response<IEnumerable<UsuarioDTO>>(null, false, errors, ResponseMessage.NotFound);
                }
            }

            await _grupoRepository.AddUsersInGroupAsync(idGrupo, usuarios);

            return new Response<IEnumerable<UsuarioDTO>>(usuarios);
        }
        catch(Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se pudo crear los usuarios en el grupo!"
            };

            return new Response<IEnumerable<UsuarioDTO>>(null, false, errors, ResponseMessage.UnexpectedErrors);
        }
    }

    public async Task<Response<GrupoDTO>> CreateGrupoAsync(GrupoInsertDTO insertDTO)
    {
        try
        {

            if(! await _usuarioRepository.CheckMonitor(insertDTO.IdMonitor))
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

    public async Task<Response<IEnumerable<GrupoDTO>>> GetAllGrupos()
    {
        try
        {
            var grupos = (await _grupoRepository.GetEntitiesAsync())
                                                .Select(x => x.MapToGrupoDTO());

            return new Response<IEnumerable<GrupoDTO>>(grupos);
        }
        catch(Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se pudo obtener los grupos!"
            };

            return new Response<IEnumerable<GrupoDTO>>(null, false, errors, ResponseMessage.UnexpectedErrors);
        }
    }

    public async Task<Response<IEnumerable<GrupoDTO>>> GetGroupsOfUsers(long idUsuario)
    {
        try
        {
            if(await _usuarioRepository.GetByIdAsync(idUsuario) is null)
            {
                var errors = new string[]
                {
                    "Este usuario no existe!",
                    "No se pudo obtener los grupos!"
                };

                return new Response<IEnumerable<GrupoDTO>>(null, false, errors, ResponseMessage.NotFound);
            }

            var groups = await _grupoRepository.GetGroupsOfUsers(idUsuario);

            return new Response<IEnumerable<GrupoDTO>>(groups);
        }
        catch(Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se pudo obtener los grupos!"
            };

            return new Response<IEnumerable<GrupoDTO>>(null, false, errors, ResponseMessage.UnexpectedErrors);
        }
    }

    public async Task<Response<IEnumerable<RecordatorioDTO>>> GetRecordatoriosAsync(long idGrupo)
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

    public async Task<Response<IEnumerable<UsuarioDTO>>> GetUsuariosAsync(long idGrupo)
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

            var errors = new string[]
            {
                "No se ha podido recuperar los usuarios",
            };

            if (grupo is null || usuario is null)
            {
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

            if (await _grupoRepository.UserExistsInGroup(idUsuario, grupo.Id))
            {
                errors = errors.Append("Este usuario ya se encuentra en este grupo!").ToArray();

                return new Response<GrupoWithUserDTO>(null, false, errors, ResponseMessage.ValidationErrors);
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

    public async Task<Response<UsuarioDTO>> LeaveGroup(long idGrupo, long idUsuario)
    {
        try
        {
            var existsUser = await _usuarioRepository.GetByIdAsync(idUsuario);
            var existsGroup = await _grupoRepository.GetByIdAsync(idGrupo);

            string[] errors =
            {
                "No se ha podido eliminar al usuario del grupo",
            };

            if (existsGroup is null || existsGroup is null)
            {

                if(existsUser is null)
                {
                    errors = errors.Append("El usuario no existe!").ToArray();
                }

                if(existsGroup is null)
                {
                    errors = errors.Append("El grupo no existe!").ToArray();
                }

                return new Response<UsuarioDTO>(null, false, errors, ResponseMessage.NotFound);
            }

            var groupWithUser = await _grupoRepository.GetGroupWithUser(idGrupo, idUsuario);
            if (groupWithUser is null)
            {
                errors = errors.Append("El usuario no está en este grupo!").ToArray();

                return new Response<UsuarioDTO>(null, false, errors, ResponseMessage.NotFound);
            }

            await _grupoRepository.LeaveGroup(groupWithUser);

            return new Response<UsuarioDTO>(existsUser.MapToUsuarioDTO());
        }
        catch(Exception e)
        {
            return new Response<UsuarioDTO>(null, false, new string[] { e.Message }, ResponseMessage.UnexpectedErrors);
        }
    }
}
