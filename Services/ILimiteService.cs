using factoring1.Models;

namespace factoring1.Services
{
    public interface ILimiteService
    {
        Task<Limite> AddLimiteAsync(int contratId, Limite limite);
        Task<IEnumerable<Limite>> GetLimitesByContratIdAsync(int contratId, int individuId); // Nouvelle méthode
        Task<decimal> GetLimitApprouvedSumByContratIdAsync(int contratId);

    }
}
