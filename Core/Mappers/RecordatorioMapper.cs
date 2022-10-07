namespace NotasAPI.Core.Mappers;

public static class RecordatorioMapper
{
    public static Recordatorio MapToRecordatorio(this RecordatorioInsertDTO recordatorioDTO, int idUsuario)
    {
        var recordatorio = new Recordatorio
        {
            Descripcion = recordatorioDTO.Descripcion,
            Fecha = recordatorioDTO.Fecha,
            IdUsuario = idUsuario,
            Prioridad = recordatorioDTO.Prioridad,
            Titulo = recordatorioDTO.Titulo
        };

        return recordatorio;
    }

    public static RecordatorioDTO MapToRecordatorioDTO(this Recordatorio recordatorio)
    {
        var recordatorioDto = new RecordatorioDTO
        {
            Descripcion = recordatorio.Descripcion,
            Fecha = recordatorio.Fecha,
            Id = recordatorio.Id,
            Prioridad = recordatorio.Prioridad,
            Titulo = recordatorio.Titulo
        };

        return recordatorioDto;
    }

    public static GrupoConRecordatorio MapToGrupoConRecordatorioDTO(this Recordatorio recordatorio, long idGrupo)
    {
        var gr = new GrupoConRecordatorio
        {
            IdGrupo = idGrupo,
            IdRecordatorio = recordatorio.Id,
        };

        return gr;
    }

    public static RecordatorioWithGroupDTO MapToRecordatorioWithGroupDTO(this GrupoConRecordatorio recordatorioG, GrupoDTO grupo, RecordatorioDTO recordatorio)
    {
        var gr = new RecordatorioWithGroupDTO
        {
            Id = recordatorioG.Id,
            GrupoDTO = grupo,
            Recordatorio = recordatorio
        };

        return gr;
    }
}
