namespace NotasAPI.Core.Business;

public class UsuarioBusiness : IUsuarioBusiness
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioBusiness(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
        
    }

    public async Task<Response<UsuarioDTO>> Register(UsuarioInsertDTO insertDTO)
    {
        try
        {
            var existsEmail = await _usuarioRepository.ExistsAsync(x => x.Correo == insertDTO.Correo);
            var existsCode = await _usuarioRepository.ExistsAsync(x => x.Codigo == insertDTO.Codigo);

            if (existsCode || existsEmail)
            {
                var errors = new string[]
                {
                    "No se pudo crear un usuario"
                };

                if (existsCode)
                {
                    errors = errors.Append("El código de estudiante ya existe!").ToArray();
                }

                if (existsEmail)
                {
                    errors = errors.Append("El correo de estudiante ya existe!").ToArray();
                }

                return new Response<UsuarioDTO>(null, false, errors, ResponseMessage.Error);
            }

            var usuario = await _usuarioRepository.CreateUsuarioAsync(insertDTO);
            return new Response<UsuarioDTO>(usuario);
        }
        catch (Exception e)
        {
            var errors = new string[]
            {
                e.Message,
                "No se pudo crear un usuario"
            };

            return new Response<UsuarioDTO>(null, false, errors, ResponseMessage.UnexpectedErrors);
        }
    }

    public async Task<Response<UsuarioDTO>> Login(UsuarioLoginDTO loginDTO)
    {
        var user = await _usuarioRepository.LoginUsuarioAsync(loginDTO);

        if(user is null)
        {
            var errors = new string[]
            {
                "El correo/código o contraseña son incorrectos!"
            };
            return new Response<UsuarioDTO>(user, false, errors, ResponseMessage.NotFound);
        }

        return new Response<UsuarioDTO>(user);
    }

    public async Task<Response<IEnumerable<UsuarioDTO>>> GetAllUsuarios()
    {
        var usuarios = await _usuarioRepository.GetEntitiesAsync();

        return new Response<IEnumerable<UsuarioDTO>>(usuarios.Select(x => x.MapToUsuarioDTO()));
    }
}
