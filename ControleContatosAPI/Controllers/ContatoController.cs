using ControleContatosAPI.Repositorios.Interaces;
using ControleContatosAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseViewModel>> BuscarTodosContatos()
        {
            ResponseViewModel response = await _contatoRepositorio.BuscarTodosContatos();

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("BuscarContatosDeUsuario/{usuarioId}")]
        public async Task<ActionResult<ResponseViewModel>> BuscarContatosDeUsuario(int usuarioId)
        {
            ResponseViewModel response = await _contatoRepositorio.BuscarContatosDeUsuario(usuarioId);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseViewModel>> BuscarPorId(int id)
        {
            ResponseViewModel response = await _contatoRepositorio.BuscarPorId(id);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseViewModel>> Adicionar([FromBody] ContatoViewModel contato)
        {
            ResponseViewModel response = await _contatoRepositorio.Adicionar(contato);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseViewModel>> Atualizar([FromBody] ContatoViewModel contato)
        {
            ResponseViewModel response = await _contatoRepositorio.Atualizar(contato);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel>> Apagar(int id)
        {
            ResponseViewModel response = await _contatoRepositorio.Apagar(id);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
