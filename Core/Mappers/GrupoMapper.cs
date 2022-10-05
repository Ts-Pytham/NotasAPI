namespace NotasAPI.Core.Mappers;

public static class GrupoMapper
{
    public static Grupo MapToGrupo(this GrupoInsertDTO grupoDTO)
    {
        var grupo = new Grupo
        {
            Codigo = grupoDTO.Codigo,
            IdMonitor = grupoDTO.IdMonitor,
            Nombre = grupoDTO.Nombre,
        };

        return grupo;
    }

    public static GrupoDTO MapToGrupoDTO(this Grupo grupo)
    {
        var grupoDto = new GrupoDTO
        {
            Codigo = grupo.Codigo,
            Id = grupo.Id,
            IdMonitor = grupo.IdMonitor,
            Nombre = grupo.Nombre
        };

        return grupoDto;
    }

}
