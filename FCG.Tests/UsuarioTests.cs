// FCG.Tests/UsuarioTests.cs

using FCG.Domain;
using Xunit; // O framework de testes

namespace FCG.Tests
{
    public class UsuarioTests
    {
        // O padrão de nomenclatura comum é: NomeMetodo_Cenario_ResultadoEsperado

        [Fact] // [Fact] indica que este método é um teste único
        public void Usuario_ComDadosValidos_DeveSerCriadoComSucesso()
        {
            // Arrange (Preparação)
            var nome = "Teste Silva";
            var email = "teste@exemplo.com";
            var senhaHash = "hash_simulado_123";
            var role = "Usuario";

            // Act (Ação)
            var usuario = new Usuario(nome, email, senhaHash, role);

            // Assert (Verificação)
            Assert.NotNull(usuario);
            Assert.Equal(nome, usuario.Nome);
            Assert.Equal(email, usuario.Email);
        }

        [Theory] // [Theory] permite rodar o mesmo teste com vários dados diferentes
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void Usuario_ComNomeInvalido_DeveLancarException(string nomeInvalido)
        {
            // Arrange
            var email = "teste@exemplo.com";
            var senhaHash = "hash";
            var role = "Usuario";

            // Act & Assert
            // Verifica se o código lança ArgumentException ao tentar criar
            Assert.Throws<ArgumentException>(() => new Usuario(nomeInvalido, email, senhaHash, role));
        }

        [Theory]
        [InlineData("emailinvalido")]
        [InlineData("email@semdominio")]
        [InlineData("@teste.com")]
        [InlineData("")]
        public void Usuario_ComEmailInvalido_DeveLancarException(string emailInvalido)
        {
            // Arrange
            var nome = "Teste";
            var senhaHash = "hash";
            var role = "Usuario";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Usuario(nome, emailInvalido, senhaHash, role));
        }

        [Fact]
        public void Usuario_SemRole_DeveLancarException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Usuario("Nome", "t@t.com", "hash", ""));
        }
    }
}