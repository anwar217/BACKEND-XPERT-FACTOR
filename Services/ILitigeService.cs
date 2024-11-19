using factoring1.Models;

namespace factoring1.Services
{
    public interface ILitigeService
    {
        Task<Litige> AddLitigeAsync(int contratId, int factureId, Litige litige);
        Task<IEnumerable<Litige>> GetLitigesByFacture(int factureId);

    }
}
