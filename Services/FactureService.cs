using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Repositories;
using factoring1.Services;
namespace factoring1.Services
{
    public class FactureService : IFactureService
    {
        private readonly IFactureRepository _factureRepository;

        public FactureService(IFactureRepository factureRepository)
        {
            _factureRepository = factureRepository;
        }

        public async Task<List<Facture>> GetFacturesByContratIdAsync(int contratId)
        {
            return await _factureRepository.GetFacturesByContratIdAsync(contratId);
        }

        public async Task<List<Facture>> GetFacturesByAcheteurAndContratIdAsync(int contratId, int acheteurId)
        {
            return await _factureRepository.GetFacturesByAcheteurAndContratIdAsync(contratId, acheteurId);
        }
        public async Task<List<Facture>> GetFacturesByBorderau(int contratId, int bordereauId,int individuId)
        {
            return await _factureRepository.GetFacturesByBordereau(contratId, bordereauId,individuId);
        }
    }
}
