using System;
using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Repositories;

namespace factoring1.Services
{
    public class FinancementService : IFinancementService
    {
        private readonly IFinancementRepository _financementRepository;
        private readonly IContratRepository _contratRepository;

        public FinancementService(IFinancementRepository financementRepository, IContratRepository contratRepository)
        {
            _financementRepository = financementRepository;
            _contratRepository = contratRepository;
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
    }
}
