using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Repositories;

namespace factoring1.Services
{
    public class ContratService : IContratService
    {
        private readonly IContratRepository _contratRepository;

        public ContratService(IContratRepository contratRepository)
        {
            _contratRepository = contratRepository;
        }

       

        public async Task<List<Contrat>> GetContratsAdherentsByIndividuId(int individuId)
        {
            var contrats = await _contratRepository.GetContratsByIndividuId(individuId);

            // Vérifier si tous les contrats sont de type Adherent
            bool tousAdherents = contrats.All(contrat =>
                contrat.IndividuContrats.Any(ic => ic.IndividuId == individuId && ic.Role == IndividuContrat.RoleType.Adherent));

            if (!tousAdherents)
            {
                return new List<Contrat>(); // Retourner une liste vide si pas tous les contrats sont adhérents
            }

            return contrats;
        }

        public async Task<List<Contrat>> GetContratsByIndividuId(int individuId)
        {
            return await _contratRepository.GetContratsByIndividuId(individuId);
        }

        public async Task<Contrat> GetContratByIndividuIdAndContratId(int individuId, int contratId)
        {
            var contrat = await _contratRepository.GetContratByIdAndIndividuId(contratId, individuId);

            if (contrat == null)
            {
                throw new ArgumentException($"Contrat avec ID {contratId} n'a pas été trouvé pour l'individu avec ID {individuId}.");
            }

            bool isAdherent = contrat.IndividuContrats.Any(ic => ic.IndividuId == individuId && ic.Role == IndividuContrat.RoleType.Adherent);
            if (!isAdherent)
            {
                throw new ArgumentException($"L'individu avec l'ID {individuId} n'est pas un adhérent pour le contrat avec ID {contratId}.");
            }

            return contrat;
        }
    }

  
}
