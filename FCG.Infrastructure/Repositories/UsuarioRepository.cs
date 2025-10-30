using FCG.Domain;
using FCG.Domain.Repositories;
using FCG.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task NovoUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            // Usa o EF Core para procurar na tabela Usuarios
            // por um usuário cujo Email seja igual ao email recebido.
            // SingleOrDefaultAsync:
            // - Retorna o usuário se encontrar exatamente um.
            // - Retorna 'null' se não encontrar nenhum.
            // - Lança uma exceção se encontrar mais de um (o que não deve acontecer
            //   graças ao nosso índice único).
            return await _context.Usuarios
                .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}