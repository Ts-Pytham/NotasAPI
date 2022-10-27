using NotasAPI.Entities;

namespace NotasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
	private readonly IUsuarioBusiness _usuarioBusiness;

	public UsuarioController(IUsuarioBusiness usuarioBusiness)
	{
		_usuarioBusiness = usuarioBusiness;
	}

	[HttpPost("register")]
	public async Task<ActionResult<Response<UsuarioDTO>>> Register(UsuarioInsertDTO usuario)
	{
		var result = await _usuarioBusiness.Register(usuario);

		if (result.Succeeded)
		{
			return Ok(result);
		}

		return BadRequest(result);
	}

    [HttpPost("login")]
    public async Task<ActionResult<Response<UsuarioWithRecordatorioDTO>>> Login(UsuarioLoginDTO usuario)
    {
        var result = await _usuarioBusiness.Login(usuario);

        if (result.Succeeded)
        {
            return Ok(result);
        }

        return NotFound(result);
    }

	[HttpGet]
	public async Task<ActionResult<Response<IEnumerable<UsuarioDTO>>>> GetUsuarios()
	{
        var result = await _usuarioBusiness.GetAllUsuarios();
        return Ok(result);
    }
}
