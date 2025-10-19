// FCG.Infrastructure/Persistence/Configurations/JogoConfiguration.cs

using FCG.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infrastructure.Persistence.Configurations
{
    public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            // Define o nome da tabela
            builder.ToTable("Jogos");

            // Define a chave primária
            builder.HasKey(j => j.Id);

            // Configura a propriedade Nome
            builder.Property(j => j.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Configura a propriedade Descricao
            builder.Property(j => j.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            // Configura a propriedade Preco
            builder.Property(j => j.Preco)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); // Especifica o tipo exato da coluna no banco
        }
    }
}