// FCG.Domain/Jogo.cs

namespace FCG.Domain
{
    public class Jogo
    {
        // Propriedades da Entidade
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }

        // Construtor para criar um novo jogo
        public Jogo(string nome, string descricao, decimal preco)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;

            // Garante que o objeto seja criado em um estado válido
            Validar();
        }

        // Método privado para as validações de regras de negócio
        private void Validar()
        {
            // Validação do Nome
            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentException("O nome do jogo não pode ser vazio ou nulo.", nameof(Nome));

            if (Nome.Length > 100) // Limite de 100 caracteres para o nome
                throw new ArgumentException("O nome do jogo não pode exceder 100 caracteres.", nameof(Nome));

            // Validação da Descrição
            if (string.IsNullOrWhiteSpace(Descricao))
                throw new ArgumentException("A descrição do jogo não pode ser vazia ou nula.", nameof(Descricao));

            if (Descricao.Length > 500) // Limite de 500 caracteres para a descrição
                throw new ArgumentException("A descrição do jogo não pode exceder 500 caracteres.", nameof(Descricao));

            // Validação do Preço
            if (Preco < 0)
                throw new ArgumentException("O preço do jogo não pode ser negativo.", nameof(Preco));
        }
    }
}