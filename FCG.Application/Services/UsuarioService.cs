
using FCG.Application.DTOs;
using FCG.Domain;
using FCG.Domain.Repositories;

namespace FCG.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task RegistrarAsync(RegistrarUsuarioRequest request)
        {

            var novoUsuario = new Usuario(
                request.Nome,
                request.Email,
                request.Senha
            );

            await _usuarioRepository.NovoUsuarioAsync(novoUsuario);
        }
    }
}