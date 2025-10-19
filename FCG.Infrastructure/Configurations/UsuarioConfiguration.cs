// FCG.Infrastructure/Persistence/Configurations/UsuarioConfiguration.cs

using FCG.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Define o nome da tabela no banco de dados
            builder.ToTable("Usuarios");

            // Define a chave primária da tabela
            builder.HasKey(u => u.Id);

            // Configura a propriedade Nome
            builder.Property(u => u.Nome)
                .IsRequired() // Garante que a coluna não pode ser nula
                .HasMaxLength(100); // Define o tamanho máximo

            // Configura a propriedade Email
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            // Cria um índice único para o Email para garantir que não haja e-mails duplicados
            builder.HasIndex(u => u.Email)
                .IsUnique();

            // Configura a propriedade Senha
            builder.Property(u => u.Senha)
                .IsRequired();
        }
    }
}