using Microsoft.AspNetCore.Mvc;

namespace NotasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordatorioController : ControllerBase
{
    private readonly IRecordatorioBusiness _recordatorioBusiness;

    public RecordatorioController(IRecordatorioBusiness recordatorioBusiness)
    {
        _recordatorioBusiness = recordatorioBusiness;
    }

    [HttpGet("{idUsuario}")]
    public async Task<ActionResult<Response<IEnumerable<RecordatorioDTO>>>> GetRecordatorios(long idUsuario)
    {
        var result = await _recordatorioBusiness.GetAllRecordatorios(idUsuario);

        if (result.Succeeded)
        {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpPost]
    public async Task<ActionResult<RecordatorioDTO>> CreateRecordatorio(long idUsuario, RecordatorioInsertDTO insertDTO)
    {
        var result = await _recordatorioBusiness.CreateRecordatorio(idUsuario, insertDTO);
        
        if (result.Succeeded)
        {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpPost("grupos/")]
    public async Task<ActionResult<Response<IEnumerable<RecordatorioWithGroupDTO>>>> CreateRecordatorioInGroup(RecordatorioWithGroupsInsertDTO insert)
    {
        var result = await _recordatorioBusiness.CreateRecordatorioInGroups(insert);

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

    [HttpPut]
    public async Task<ActionResult<Response<RecordatorioUpdateDTO>>> UpdateRecordatorio([FromBody] RecordatorioUpdateDTO updateDTO)
    {
        var result = await _recordatorioBusiness.UpdateRecordatorio(updateDTO);

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

    [HttpDelete("{idRecordatorio}")]
    public async Task<ActionResult<Response<RecordatorioDeleteDTO>>> DeleteRecordatorio(long idRecordatorio)
    {
        var result = await _recordatorioBusiness.DeleteRecordatorio(idRecordatorio);

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
