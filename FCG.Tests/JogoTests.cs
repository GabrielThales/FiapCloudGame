// FCG.Tests/JogoTests.cs

using FCG.Domain;
using Xunit;

namespace FCG.Tests
{
    public class JogoTests
    {
        [Fact]
        public void Jogo_ComDadosValidos_DeveSerCriado()
        {
            // Arrange
            var nome = "Super Mario";
            var descricao = "Jogo de plataforma";
            decimal preco = 199.90m;

            // Act
            var jogo = new Jogo(nome, descricao, preco);

            // Assert
            Assert.NotNull(jogo);
            Assert.Equal(preco, jogo.Preco);
        }

        [Fact]
        public void Jogo_ComPrecoNegativo_DeveLancarException()
        {
            // Arrange
            decimal precoNegativo = -10.00m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Jogo("Nome", "Descricao", precoNegativo));
        }

        [Fact]
        public void Jogo_ComNomeVazio_DeveLancarException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Jogo("", "Descricao", 100));
        }

        [Fact]
        public void Jogo_ComDescricaoMuitoLonga_DeveLancarException()
        {
            // Arrange
            // Cria uma string com 501 caracteres 'A'
            var descricaoLonga = new string('A', 501);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Jogo("Nome", descricaoLonga, 100));
        }
    }
}