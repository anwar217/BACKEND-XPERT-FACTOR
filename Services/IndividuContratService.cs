﻿using factoring1.DTO;
using factoring1.Models;
using factoring1.Repositories;
using System.Threading.Tasks;
using static factoring1.Models.IndividuContrat;

namespace factoring1.Services
{
    public class IndividuContratService : IIndividuContratService
    {
        private readonly IIndividuContratRepository _individuContratRepository;

        public IndividuContratService(IIndividuContratRepository individuContratRepository)
        {
            _individuContratRepository = individuContratRepository;
        }

        public async Task<bool> IsAdherent(int contratId)
        {
            var individuContrat = await _individuContratRepository.GetIndividuContratByContratId(contratId);
            return individuContrat?.Role == IndividuContrat.RoleType.Adherent;
        }

        public async Task<bool> AjouterAcheteursAuContrat(int adherentId, int contratId, List<int> acheteurIds)
        {
            // Vérifier si l'adhérent est le propriétaire du contrat
            var estProprietaire = await _individuContratRepository.EstProprietaireDuContrat(adherentId, contratId);
            if (!estProprietaire)
            {
                // Si ce n'est pas le propriétaire du contrat, retourner une erreur
                throw new UnauthorizedAccessException("Vous n'êtes pas autorisé à modifier ce contrat.");
            }

            // Pour chaque acheteur, vérifier s'il est déjà présent et l'ajouter s'il ne l'est pas
            foreach (var acheteurId in acheteurIds)
            {
                var acheteurDejaPresent = await _individuContratRepository.AcheteurDejaPresent(contratId, acheteurId);
                if (acheteurDejaPresent)
                {
                    throw new InvalidOperationException($"L'acheteur avec ID {acheteurId} est déjà assigné à ce contrat.");
                }

                // Ajouter chaque acheteur au contrat
                var nouvelAcheteur = new IndividuContrat
                {
                    ContratId = contratId,
                    IndividuId = acheteurId,
                    Role = RoleType.Acheteur
                };

                await _individuContratRepository.AjouterIndividuContrat(nouvelAcheteur);
            }

            return true;
        }
        public async Task<bool> LinkContractToAdherent(int contractId, int adherentId)
        {
            await _individuContratRepository.LinkContractToAdherent(contractId, adherentId);
            return true;
        }

     

    }
}
