using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.DTO;
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

        public async Task<List<FactureWithCountDto>> GetFacturesByAcheteurAndContratIdAsync(int contratId, int acheteurId)
        {
            return await _factureRepository.GetFacturesByAcheteurAndContratIdAsync(contratId, acheteurId);
        }
        public async Task<List<Facture>> GetFacturesByBorderau(int contratId, int bordereauId,int individuId)
        {
            return await _factureRepository.GetFacturesByBordereau(contratId, bordereauId,individuId);
        }
        public async Task<decimal> GetFactureEnCoursByContratIdAsync(int contratId){
            return await _factureRepository.GetFactureEnCoursByContratIdAsync(contratId);
        }

        public async Task<decimal> GetFacturesApprouvedByContratIdAsync(int contratId){
            return await _factureRepository.GetFacturesApprouvedByContratIdAsync(contratId);
        }
        public async Task<Facture> GetFacturesWithLitigesAsync(int factureId){
            return await _factureRepository.GetFacturesWithLitigesAsync(factureId);
        }
        public async Task<Facture> GetFacturesWithProrogationsAsync(int factureId){
            return await _factureRepository.GetFacturesWithProrogationsAsync(factureId);
        }
    }
}
