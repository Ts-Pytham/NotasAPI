namespace NotasAPI.Core.Models.DTOs.Recordatorio;

public class RecordatorioWithGroupsInsertDTO
{
    public long[] IdGrupos { get; set; }

    public long IdMonitor { get; set; }

    public RecordatorioInsertDTO Recordatorio { get; set; }
}
