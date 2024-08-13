using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.Models;

namespace factoring1.Services
{
    public interface IContratService
    {
        Task<List<Contrat>> GetContratsAdherentsByIndividuId(int individuId);
        Task<List<Contrat>> GetContratsByIndividuId(int individuId);
        Task<Contrat> GetContratByIndividuIdAndContratId(int individuId, int contratId);

        // Autres méthodes de service
    }
}
