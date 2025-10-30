using FCG.Domain;
using FCG.Domain.Repositories;
using FCG.Infrastructure.Persistence;

namespace FCG.Infrastructure.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly AppDbContext _context;

        public JogoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task NovoJogoAsync(Jogo jogo)
        {
            await _context.Jogos.AddAsync(jogo);
            await _context.SaveChangesAsync();
        }
    }
}