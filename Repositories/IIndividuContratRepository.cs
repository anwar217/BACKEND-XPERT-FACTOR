using System.Threading.Tasks;
using factoring1.Models;

namespace factoring1.Repositories
{
    public interface IIndividuContratRepository
    {
        Task<IndividuContrat> GetIndividuContratByContratId(int contratId);
    }
}
