using System.Text.RegularExpressions;

namespace FCG.Domain
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string Role { get; private set; }

        private Usuario() { }

        public Usuario(string nome, string email, string senhaHash, string role)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Senha = senhaHash;
            Role = role;

            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentException("O nome não pode ser vazio ou nulo.", nameof(Nome));

            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("O e-mail não pode ser vazio ou nulo.", nameof(Email));

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(Email))
                throw new ArgumentException("O formato do e-mail é inválido.", nameof(Email));

            if (string.IsNullOrWhiteSpace(Senha))
                throw new ArgumentException("O hash da senha não pode ser vazio ou nulo.", nameof(Senha));

            if (string.IsNullOrWhiteSpace(Role))
                throw new ArgumentException("A role (nível de acesso) não pode ser vazia.", nameof(Role));
        }
    }
}