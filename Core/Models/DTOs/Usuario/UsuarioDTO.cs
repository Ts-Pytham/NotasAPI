namespace NotasAPI.Core.Models.DTOs.Usuario;

public class UsuarioDTO
{
    public long Id { get; set; }

    public string Nombre { get; set; }

    public string Correo { get; set; }
    
    public int Codigo { get; set; }

    public string Rol { get; set; }
}
