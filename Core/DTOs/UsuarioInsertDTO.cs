namespace NotasAPI.Core.DTOs;

public class UsuarioInsertDTO
{
    [Required]
    public string Nombre { get; set; }

    [Required]
    public string Correo { get; set; }

    [Required]
    public string Contraseña { get; set; }

    [Required]
    public string Código { get; set; }

}
