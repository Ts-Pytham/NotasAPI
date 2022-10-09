namespace NotasAPI.Core.Interfaces;

public interface IUsuarioBusiness
{
    public Task<Response<UsuarioDTO>> Register(UsuarioInsertDTO insertDTO);

    public Task<Response<UsuarioCookies>> Login(UsuarioLoginDTO loginDTO);

    public Task<Response<IEnumerable<UsuarioDTO>>> GetAllUsuarios();
}
