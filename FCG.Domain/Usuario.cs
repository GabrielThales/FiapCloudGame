// FCG.Domain/Usuario.cs

using System.Text.RegularExpressions;

namespace FCG.Domain
{
    public class Usuario
    {
        // Propriedades da Entidade
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        // Futuramente, podemos adicionar: public string Role { get; private set; } (Usuário ou Administrador)

        // Construtor usado para criar um novo usuário
        public Usuario(string nome, string email, string senha)
        {
            // Geramos um novo ID único para o usuário
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Senha = senha; // A senha será armazenada como hash mais tarde!

            // Chamamos a validação para garantir que o objeto está num estado válido
            Validar();
        }

        // Método privado para validar as regras de negócio (invariantes)
        private void Validar()
        {
            // Validação do Nome
            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentException("O nome não pode ser vazio ou nulo.", nameof(Nome));

            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("O e-mail não pode ser vazio ou nulo.", nameof(Email));

            // Usamos uma expressão regular (Regex) simples para validar o formato do e-mail
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(Email))
                throw new ArgumentException("O formato do e-mail é inválido.", nameof(Email));

            if (string.IsNullOrWhiteSpace(Senha))
                throw new ArgumentException("A senha não pode ser vazia ou nula.", nameof(Senha));

            if (Senha.Length < 8)
                throw new ArgumentException("A senha deve ter no mínimo 8 caracteres.", nameof(Senha));

            if (!Senha.Any(char.IsDigit))
                throw new ArgumentException("A senha deve conter pelo menos um número.", nameof(Senha));

            if (!Senha.Any(char.IsLetter))
                throw new ArgumentException("A senha deve conter pelo menos uma letra.", nameof(Senha));

            // Verifica se tem caractere especial
            if (!Regex.IsMatch(Senha, @"[!@#$%^&*(),.?""{}|<>]"))
                throw new ArgumentException("A senha deve conter pelo menos um caractere especial.", nameof(Senha));
        }
    }
}