using FCG.Application.DTOs;
using FCG.Domain;
using FCG.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace FCG.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenService _tokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task RegistrarAsync(RegistrarUsuarioRequest request)
        {
            ValidarSenha(request.Senha);

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            /*var novoUsuario = new Usuario(
                request.Nome,
                request.Email,
                senhaHash,
                "Usuario"
            );*/

            //Criação de usuário ADM
            var novoUsuario = new Usuario(request.Nome, request.Email, senhaHash, "Administrador");

            await _usuarioRepository.NovoUsuarioAsync(novoUsuario);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // 1. Buscar o usuário pelo e-mail
            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            // 2. Verificar se o usuário existe
            if (usuario == null)
            {
                return null; // Retorna null se não encontrar (trataremos como erro no Controller)
            }

            // 3. Verificar se a senha bate com o Hash salvo
            //    O BCrypt pega a senha "texto puro" (request.Senha) e verifica se
            //    ela gera o mesmo hash que está no banco (usuario.Senha).
            if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
            {
                return null; // Senha incorreta
            }

            // 4. Se chegou aqui, o login é válido! Geramos o token.
            var token = _tokenService.GerarToken(usuario);

            // 5. Retornamos o DTO de resposta
            return new LoginResponse
            {
                AccessToken = token,
                Validade = DateTime.UtcNow.AddHours(2)
            };
        }

        private void ValidarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("A senha não pode ser vazia ou nula.", nameof(senha));

            if (senha.Length < 8)
                throw new ArgumentException("A senha deve ter no mínimo 8 caracteres.", nameof(senha));

            if (!senha.Any(char.IsDigit))
                throw new ArgumentException("A senha deve conter pelo menos um número.", nameof(senha));

            if (!senha.Any(char.IsLetter))
                throw new ArgumentException("A senha deve conter pelo menos uma letra.", nameof(senha)); 

            if (!Regex.IsMatch(senha, @"[!@#$%^&*(),.?""{}|<>]"))
                throw new ArgumentException("A senha deve conter pelo menos um caractere especial.", nameof(senha));
        }
    }
}