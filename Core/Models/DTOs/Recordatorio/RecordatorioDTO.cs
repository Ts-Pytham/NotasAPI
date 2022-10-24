namespace NotasAPI.Core.Models.DTOs.Recordatorio;

public class RecordatorioDTO : RecordatorioInsertDTO
{
    public long Id { get; set; }

    public string Autor { get; set; }
}
