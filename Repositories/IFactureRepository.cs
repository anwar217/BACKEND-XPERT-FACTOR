using factoring1.Models;

namespace factoring1.Repositories
{
    public interface IFactureRepository
    {
        Task<List<Facture>> GetFacturesByContratIdAsync(int contratId);

    }
}
