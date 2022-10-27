namespace NotasAPI.Core.Mappers;

public static class UsuarioMapper
{
    public static Usuario MapToUsuario(this UsuarioInsertDTO usuarioDTO)
    {
        var usuario = new Usuario
        {
            Codigo = usuarioDTO.Codigo,
            Nombre = usuarioDTO.Nombre,
            Correo = usuarioDTO.Correo,
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

    public static GrupoConUsuario MapToGrupoConUsuario(this UsuarioDTO usuario, long idgrupo)
    {
        var grupo = new GrupoConUsuario
        {
            IdGrupo = idgrupo,
            IdUsuario = usuario.Id
        };

        return grupo;
    }

    public static GrupoWithUserDTO MapToGrupoWithUserDTO(this UsuarioDTO usuario, GrupoDTO grupoDto)
    {
        var g = new GrupoWithUserDTO
        {
            GrupoDTO = grupoDto,
            UsuarioDTO = usuario
        };

        return g;
    }

    public static UsuarioWithRecordatorioDTO MapToUsuarioWithRecordatorioDTO(this UsuarioDTO usuario, IEnumerable<RecordatorioDTO> recordatorios)
    {
        var usuarioWR = new UsuarioWithRecordatorioDTO
        {
            Codigo = usuario.Codigo,
            Id = usuario.Id,
            Correo = usuario.Correo,
            Nombre = usuario.Nombre,
            Recordatorios = recordatorios,
            Rol = usuario.Rol
        };

        return usuarioWR;
    }
}
