namespace NotasAPI.Core.Mappers;

public static class UsuarioMapper
{
    public static Usuario MapToUsuario(this UsuarioInsertDTO usuarioDTO)
    {
        var usuario = new Usuario
        {
            Codigo = usuarioDTO.Codigo,
            Nombre = usuarioDTO.Nombre,
            IdRol = 1,
            Contraseña = usuarioDTO.Contraseña.HashPasswordBCrypt()
        };

        return usuario;
    }

    public static UsuarioDTO MapToUsuarioWithRolDTO(this Usuario usuario)
    {
        var usuarioDTO = new UsuarioDTO
        {
            Codigo = usuario.Codigo,
            Id = usuario.Id,
            Correo = usuario.Correo,
            Nombre = usuario.Nombre,
            Rol = usuario.IdRolNavigation.Nombre
        };

        return usuarioDTO;
    }
    public static UsuarioDTO MapToUsuarioDTO(this Usuario usuario)
    {
        var usuarioDTO = new UsuarioDTO
        {
            Codigo = usuario.Codigo,
            Correo = usuario.Correo,
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Rol = ((RolEnum)usuario.IdRol).GetDisplayName()
            
        };

        return usuarioDTO;
    }
}
