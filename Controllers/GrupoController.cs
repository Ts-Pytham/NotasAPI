using NotasAPI.Entities;

namespace NotasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GrupoController : ControllerBase
{
    private readonly IGrupoBusiness _grupoBusiness;

    public GrupoController(IGrupoBusiness grupoBusiness)
    {
        _grupoBusiness = grupoBusiness;
    }

    [HttpGet("recordatorios/{idGrupo}")]
    public async Task<ActionResult<Response<IEnumerable<RecordatorioDTO>>>> GetRecordatorios(int idGrupo)
    {
        var result = await _grupoBusiness.GetRecordatoriosAsync(idGrupo);

        if (result.Succeeded)
        {
            return Ok(result);
        }
        
        if(result.Message == ResponseMessage.NotFound)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }

    [HttpGet("usuarios/{idGrupo}")]
    public async Task<ActionResult<Response<IEnumerable<UsuarioDTO>>>> GetUsuarios(int idGrupo)
    {
        var result = await _grupoBusiness.GetUsuariosAsync(idGrupo);

        if (result.Succeeded)
        {
            return Ok(result);
        }

        if (result.Message == ResponseMessage.NotFound)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }

    [HttpPost]
    public async Task<ActionResult<Response<GrupoDTO>>> CreateGrupo(GrupoInsertDTO insertDTO)
    {
        var result = await _grupoBusiness.CreateGrupoAsync(insertDTO);

        if (result.Succeeded)
        {
            return Ok(result);
        }

        if (result.Message == ResponseMessage.NotFound)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }

    [HttpPost("{idCodigo}/{idUsuario}")]
    public async Task<ActionResult<Response<GrupoWithUserDTO>>> InsertToGrupo(int idCodigo, long idUsuario)
    {
        var result = await _grupoBusiness.InsertToGrupoAsync(idCodigo,idUsuario);

        if (result.Succeeded)
        {
            return Ok(result);
        }

        if (result.Message == ResponseMessage.NotFound)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }

}
