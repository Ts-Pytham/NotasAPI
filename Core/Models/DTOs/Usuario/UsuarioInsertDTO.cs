namespace NotasAPI.Core.Models.DTOs.Usuario;

public class UsuarioInsertDTO
{
    [Required]
    public string Nombre { get; set; }

    [Required, EmailAddress]
    public string Correo { get; set; }

    [Required]
    public string Contraseña { get; set; }

    [Required]
    public int Codigo { get; set; }

}
