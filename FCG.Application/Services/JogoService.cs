using FCG.Application.DTOs;
using FCG.Domain;
using FCG.Domain.Repositories;

namespace FCG.Application.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task RegistrarAsync(RegistrarJogoRequest request)
        {

            var novoJogo = new Jogo(
                request.Nome,
                request.Descricao,
                request.Preco
            );

            await _jogoRepository.NovoJogoAsync(novoJogo);
        }
    }
}