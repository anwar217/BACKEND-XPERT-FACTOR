using System.Threading.Tasks;
using factoring1.Models;

namespace factoring1.Repositories
{
    public interface IIndividuContratRepository
    {
        Task<IndividuContrat> GetIndividuContratByContratId(int contratId);
        Task<bool> EstProprietaireDuContrat(int adherentId, int contratId);
        Task<bool> AcheteurDejaPresent(int contratId, int acheteurId);
        Task AjouterIndividuContrat(IndividuContrat individuContrat);
        Task LinkContractToAdherent(int contractId, int adherentId);
    }
}
