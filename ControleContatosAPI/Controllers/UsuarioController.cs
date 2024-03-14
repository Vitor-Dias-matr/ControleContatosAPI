using ControleContatosAPI.Repositorios.Interaces;
using ControleContatosAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseViewModel>> BuscarTodosUsuarios()
        {
            ResponseViewModel response = await _usuarioRepositorio.BuscarTodosUsuarios();

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseViewModel>> BuscarPorId(int id)
        {
            ResponseViewModel response = await _usuarioRepositorio.BuscarPorId(id);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("BuscarPorLogin/{login}")]
        public async Task<ActionResult<ResponseViewModel>> BuscarPorLogin(string login)
        {
            ResponseViewModel response = await _usuarioRepositorio.BuscarPorLogin(login);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("BuscarPorEmailELogin/{email}&{login}")]
        public async Task<ActionResult<ResponseViewModel>> BuscarPorEmailELogin(string email, string login)
        {
            ResponseViewModel response = await _usuarioRepositorio.BuscarPorEmailELogin(email, login);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseViewModel>> Adicionar([FromBody] UsuarioViewModel usuario)
        {
            ResponseViewModel response = await _usuarioRepositorio.Adicionar(usuario);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseViewModel>> Atualizar([FromBody] UsuarioSemSenhaViewModel usuarioSemSenha)
        {
            ResponseViewModel response = await _usuarioRepositorio.Atualizar(usuarioSemSenha);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel>> Apagar(int id)
        {
            ResponseViewModel response = await _usuarioRepositorio.Apagar(id);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("AlterarSenhaUsuario")]
        public async Task<ActionResult<ResponseViewModel>> AlterarSenhaUsuario([FromBody] AlterarSenhaViewModel alterarSenha)
        {
            ResponseViewModel response = await _usuarioRepositorio.AlterarSenhaUsuario(alterarSenha);

            if (response.status.code == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}