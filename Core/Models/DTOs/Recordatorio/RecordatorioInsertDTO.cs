namespace NotasAPI.Core.Models.DTOs.Recordatorio;

public class RecordatorioInsertDTO
{
    [Required, MaxLength(100, ErrorMessage = "El mensaje debe tener como máximo 100 caracteres!")]
    public string Titulo { get; set; }

    [Required, MaxLength(255, ErrorMessage = "El mensaje debe tener como máximo 255 caracteres!")]
    public string Descripcion { get; set; }

    [Required]
    public string Prioridad { get; set; }

    [Required]
    public DateTime Fecha { get; set; }
}
