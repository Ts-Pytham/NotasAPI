namespace NotasAPI.Core.Models.DTOs.Usuario;

public class UsuarioWithRecordatorioDTO : UsuarioDTO
{
    public IEnumerable<RecordatorioDTO> Recordatorios { get; set; }
}
