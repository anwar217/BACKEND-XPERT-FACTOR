using factoring1.DTO;
using factoring1.Models;
using static factoring1.Services.FactureRepository;

namespace factoring1.Repositories
{
    public interface IFactureRepository
    {
        Task<List<Facture>> GetFacturesByContratIdAsync(int contratId);
        Task<List<FactureWithCountDto>> GetFacturesByAcheteurAndContratIdAsync(int contratId, int acheteurId);
        Task<List<Facture>> GetFacturesByBordereau(int contratId, int bordereauId,int individuId);

    }
}
