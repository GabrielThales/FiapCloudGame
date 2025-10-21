using FCG.Application.DTOs;
using FCG.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers
{
    [ApiController] // Marca esta classe como um Controller de API
    [Route("api/[controller]")] // Define a rota base: "http://localhost/api/usuarios"
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        // Pedimos o UsuarioService via injeção de dependência
        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar")] // Define o endpoint: POST http://localhost/api/usuarios/registrar
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioRequest request)
        {
            try
            {
                // 1. Chama o serviço da camada de aplicação
                await _usuarioService.RegistrarAsync(request);

                // 2. Se tudo correu bem, retorna um "201 Created" (sucesso na criação)
                // (Idealmente, retornaríamos um link para o recurso criado, mas Ok() é suficiente por agora)
                return Ok("Usuário registrado com sucesso!");
            }
            catch (ArgumentException ex)
            {
                // 3. Se a validação no Domínio falhou (ex: senha fraca, email inválido),
                //    o construtor do Usuario lançou uma ArgumentException.
                //    Capturamos essa exceção e retornamos um "400 Bad Request"
                return BadRequest(new { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                // 4. Captura qualquer outro erro inesperado (ex: falha ao salvar no banco)
                //    e retorna um "500 Internal Server Error"
                return StatusCode(500, new { Mensagem = $"Ocorreu um erro interno: {ex.Message}" });
            }
        }
    }
}