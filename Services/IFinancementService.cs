using System.Threading.Tasks;
using factoring1.Models;

namespace factoring1.Services
{
    public interface IFinancementService
    {
        Task<Financement> AddFinancement(int individuId, Financement financement);
    }
}
