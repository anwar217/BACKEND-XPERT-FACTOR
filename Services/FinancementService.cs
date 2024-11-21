using System;
using System.Threading.Tasks;
using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using factoring1.Repositories;
using Microsoft.EntityFrameworkCore;

namespace factoring1.Services
{
    public class FinancementService : IFinancementService
    {
        private readonly IFinancementRepository _financementRepository;
        private readonly IContratRepository _contratRepository;
        private readonly FactoringDbContext _context;


        public FinancementService(IFinancementRepository financementRepository, IContratRepository contratRepository, FactoringDbContext context)
        {
            _financementRepository = financementRepository;
            _contratRepository = contratRepository;
            _context = context;

        }

        public async Task<Financement> AddFinancement(int individuId, Financement financement)
        {
            bool isAdherent = await _contratRepository.IsIndividuAdherentForContrat(individuId, financement.ContratId);

            if (!isAdherent)
            {
                throw new ArgumentException($"L'individu avec l'ID {individuId} n'est pas un adhérent pour le contrat avec ID {financement.ContratId}.");
            }

            await _financementRepository.Add(financement);

            return financement;
        }
        public async Task<List<Financement>> GetFinancementsByContratAndIndividuIdAsync(int contratId, int individuId)
        {
            // Rechercher les IndividuContrat pour récupérer les contrats associés à l'IndividuId
            var individuContrats = await _context.IndividuContrats
                .Where(ic => ic.IndividuId == individuId && ic.ContratId == contratId)
                .ToListAsync();

            if (individuContrats.Count == 0)
            {
                // Aucun individuContrat trouvé pour l'IndividuId et ContratId donnés
                return new List<Financement>(); // Pas de financements
            }

            // Récupérer les financements associés à ce contrat
            var financements = await _context.Financements
                .Where(f => f.ContratId == contratId)
                .ToListAsync();

            return financements;
        }
    }
}
