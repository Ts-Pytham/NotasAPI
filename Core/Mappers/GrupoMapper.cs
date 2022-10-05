namespace NotasAPI.Core.Mappers;

public static class GrupoMapper
{
    public static Grupo MapToGrupo(this GrupoInsertDTO grupoDTO, int codigo)
    {
        var grupo = new Grupo
        {
            IdMonitor = grupoDTO.IdMonitor,
            Nombre = grupoDTO.Nombre,
            Codigo = codigo,
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
