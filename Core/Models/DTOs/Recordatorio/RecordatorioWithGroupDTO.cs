namespace NotasAPI.Core.Models.DTOs.Recordatorio;

public class RecordatorioWithGroupDTO
{
    public long Id { get; set; }

    public RecordatorioDTO Recordatorio { get; set; }

    public long IdGrupo { get; set; }
}
