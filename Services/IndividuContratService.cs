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

        public async Task<bool> AjouterAcheteurAuContrat(int adherentId, int contratId, int acheteurId)
        {
            // Vérifier si l'adhérent est le propriétaire du contrat
            var estProprietaire = await _individuContratRepository.EstProprietaireDuContrat(adherentId, contratId);
            if (!estProprietaire)
            {
                // Si ce n'est pas le propriétaire du contrat, retourner une erreur
                throw new UnauthorizedAccessException("Vous n'êtes pas autorisé à modifier ce contrat.");
            }

            // Vérifier si l'acheteur est déjà assigné au contrat
            var acheteurDejaPresent = await _individuContratRepository.AcheteurDejaPresent(contratId, acheteurId);
            if (acheteurDejaPresent)
            {
                // Acheteur déjà présent, lancer une exception
                throw new InvalidOperationException("Cet acheteur est déjà assigné à ce contrat.");
            }

            // Ajouter l'acheteur au contrat
            var nouvelAcheteur = new IndividuContrat
            {
                ContratId = contratId,
                IndividuId = acheteurId,
                Role = RoleType.Acheteur
            };

            await _individuContratRepository.AjouterIndividuContrat(nouvelAcheteur);

            return true;
        }
    }
}
