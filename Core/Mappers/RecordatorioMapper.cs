namespace NotasAPI.Core.Mappers;

public static class RecordatorioMapper
{
    public static Recordatorio MapToRecordatorio(this RecordatorioInsertDTO recordatorioDTO, long idUsuario)
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

    public static RecordatorioDTO MapToRecordatorioDTO(this Recordatorio recordatorio, string autor)
    {
        var recordatorioDto = new RecordatorioDTO
        {
            Descripcion = recordatorio.Descripcion,
            Fecha = recordatorio.Fecha,
            Id = recordatorio.Id,
            Prioridad = recordatorio.Prioridad,
            Titulo = recordatorio.Titulo,
            Autor =  autor,
            
        };

        return recordatorioDto;
    }

    public static GrupoConRecordatorio MapToGrupoConRecordatorioDTO(this Recordatorio recordatorio, long idGrupo)
    {
        var gr = new GrupoConRecordatorio
        {
            IdGrupo = idGrupo,
            IdRecordatorioNavigation = recordatorio,
        };

        return gr;
    }

    public static RecordatorioWithGroupDTO MapToRecordatorioWithGroupDTO(this GrupoConRecordatorio recordatorioG, long IdGrupo, RecordatorioDTO recordatorio)
    {
        var gr = new RecordatorioWithGroupDTO
        {
            Id = recordatorioG.Id,
            IdGrupo = IdGrupo,
            Recordatorio = recordatorio
        };

        return gr;
    }

    public static RecordatorioDeleteDTO MapToRecordatorioDeleteDTO(this Recordatorio recordatorio)
    {
        var deleteDTO = new RecordatorioDeleteDTO
        {
            Descripcion = recordatorio.Descripcion,
            Fecha = recordatorio.Fecha,
            Prioridad = recordatorio.Prioridad,
            Titulo = recordatorio.Titulo,
        };

        return deleteDTO;
    }
    public static void SetToRecordatorio(this Recordatorio recordatorio, RecordatorioUpdateDTO updateDTO)
    {
        recordatorio.Prioridad = updateDTO.Prioridad;
        recordatorio.Descripcion = updateDTO.Descripcion;
        recordatorio.Fecha = updateDTO.Fecha;
        recordatorio.Titulo = updateDTO.Titulo;
    }
}
