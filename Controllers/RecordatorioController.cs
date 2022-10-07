namespace NotasAPI.Controllers
{
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
        public async Task<ActionResult<Response<IEnumerable<RecordatorioDTO>>>> GetRecordatorios(int idUsuario)
        {
            var result = await _recordatorioBusiness.GetAllRecordatorios(idUsuario);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<RecordatorioInsertDTO>> CreateRecordatorio(int idUsuario, RecordatorioInsertDTO insertDTO)
        {
            var result = await _recordatorioBusiness.CreateRecordatorio(idUsuario, insertDTO);
            
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpPost("grupos/{idGrupo}/{idUsuario}")]
        public async Task<ActionResult<Response<RecordatorioWithGroupDTO>>> CreateRecordatorioInGroup(int idGrupo, int idUsuario, RecordatorioInsertDTO insertDTO)
        {
            var result = await _recordatorioBusiness.CreateRecordatorioInGroup(idUsuario, idGrupo, insertDTO);

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
}
