using factoring1.Models;

namespace factoring1.Repositories
{
    public interface ILimiteRepository
    {
        Task<Limite> AddLimiteAsync(Limite limite);
        Task<Contrat> GetContratByIdAsync(int contratId);

    }
}
