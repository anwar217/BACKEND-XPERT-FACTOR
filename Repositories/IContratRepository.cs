using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.Models;

namespace factoring1.Repositories
{
    public interface IContratRepository
    {
        Task<List<Contrat>> GetContratsByIndividuId(int individuId);
        Task<Contrat> GetContratByIdAndIndividuId(int contratId, int individuId);

        Task<bool> IsIndividuAdherentForContrat(int individuId, int contratId);
        Task<Contrat> AddContratForIndividuAsync(int contratId, int individuId);
        Task<List<Contrat>> GetAllContratsAsync();

    }
}
