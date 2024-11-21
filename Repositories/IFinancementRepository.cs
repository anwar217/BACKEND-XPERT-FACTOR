using System.Threading.Tasks;
using factoring1.Models;

namespace factoring1.Repositories
{
    public interface IFinancementRepository
    {
        Task Add(Financement financement);
        Task<List<Financement>> GetFinancementsByContratIdAsync(int contratId);

    }
}
