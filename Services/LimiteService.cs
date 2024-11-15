using factoring1.Models;
using factoring1.Repositories;

namespace factoring1.Services
{
     public class LimiteService : ILimiteService
    {
        private readonly ILimiteRepository _limiteRepository;

        public LimiteService(ILimiteRepository limiteRepository)
        {
            _limiteRepository = limiteRepository;
        }

        public async Task<Limite> AddLimiteAsync(int contratId, Limite limite)
        {
            var contrat = await _limiteRepository.GetContratByIdAsync(contratId);
            if (contrat == null)
            {
                throw new ArgumentException("Contrat not found");
            }

            limite.ContratId = contratId;
            return await _limiteRepository.AddLimiteAsync(limite);
        }
        public async Task<IEnumerable<Limite>> GetLimitesByContratIdAsync(int contratId)
        {
            return await _limiteRepository.GetLimitesByContratIdAsync(contratId);
        }
    }
}
