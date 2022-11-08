using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<Response<IEnumerable<RecordatorioDTO>>>> GetRecordatorios(long idGrupo)
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
    public async Task<ActionResult<Response<IEnumerable<UsuarioDTO>>>> GetUsuarios(long idGrupo)
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


    [HttpGet("usuarios/{idUsuario}/grupos")]
    public async Task<ActionResult<Response<IEnumerable<UsuarioDTO>>>> GetGroupsOfUsers(long idUsuario)
    {
        var result = await _grupoBusiness.GetGroupsOfUsers(idUsuario);

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

    [HttpPost("{idGrupo}")]
    public async Task<ActionResult<Response<IEnumerable<UsuarioDTO>>>> AddUsersInGroup(long idGrupo, IEnumerable<UsuarioDTO> usuarios)
    {
        var result = await _grupoBusiness.AddUsersInGroup(idGrupo, usuarios);

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

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<GrupoDTO>>>> GetGrupos()
    {
        var result = await _grupoBusiness.GetAllGrupos();
        if (result.Succeeded)
        {
            return Ok(result);
        }

        return BadRequest(result);
        
    }

    [HttpDelete("{idGrupo}/usuarios/{idUsuario}")]
    public async Task<ActionResult<Response<UsuarioDTO>>> LeaveGroup(long idGrupo, long idUsuario)
    {
        var result = await _grupoBusiness.LeaveGroup(idGrupo, idUsuario);

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
}
