namespace NotasAPI.Core.Models.DTOs.Recordatorio;

public class RecordatorioInsertDTO
{
    [Required]
    public string Titulo { get; set; }

    [Required]
    public string Descripcion { get; set; }

    [Required]
    public string Prioridad { get; set; }

    [Required]
    public DateTime Fecha { get; set; }
}
