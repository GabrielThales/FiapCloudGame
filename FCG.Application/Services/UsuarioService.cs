using FCG.Application.DTOs;
using FCG.Domain;
using FCG.Domain.Repositories;
using System.Text.RegularExpressions;

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
            ValidarSenha(request.Senha);

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            var novoUsuario = new Usuario(
                request.Nome,
                request.Email,
                senhaHash,
                "Usuario"
            );

            await _usuarioRepository.NovoUsuarioAsync(novoUsuario);
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