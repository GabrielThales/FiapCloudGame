// FCG.Infrastructure/Persistence/AppDbContext.cs

using FCG.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection; // Adicione este using

namespace FCG.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jogo> Jogos { get; set; }

        // Agora este método fica muito mais limpo!
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Este comando varre todo o projeto (assembly) em busca de classes
            // que implementam IEntityTypeConfiguration e aplica todas elas de uma vez.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}