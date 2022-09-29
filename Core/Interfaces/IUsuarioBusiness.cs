namespace NotasAPI.Core.Interfaces;

public interface IUsuarioBusiness
{
    public Task<Response<UsuarioDTO>> Register(UsuarioInsertDTO insertDTO);

    public Task<Response<UsuarioDTO>> Login(UsuarioLoginDTO loginDTO);
}
