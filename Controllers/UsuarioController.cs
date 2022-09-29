﻿namespace NotasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
	private readonly IUsuarioBusiness _usuarioBusiness;

	public UsuarioController(IUsuarioBusiness usuarioBusiness)
	{
		_usuarioBusiness = usuarioBusiness;
	}

	[HttpPost]
	public async Task<ActionResult<Response<UsuarioDTO>>> Register(UsuarioInsertDTO usuario)
	{
		var result = await _usuarioBusiness.Register(usuario);

		if (result.Succeeded)
		{
			return Ok(result);
		}

		return BadRequest(result);
	}

    [HttpPost]
    public async Task<ActionResult<Response<UsuarioDTO>>> Login(UsuarioLoginDTO usuario)
    {
        var result = await _usuarioBusiness.Login(usuario);

        if (result.Succeeded)
        {
            return Ok(result);
        }

        return NotFound(result);
    }
}