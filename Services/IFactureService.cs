using factoring1.DTO;
using factoring1.Models;

namespace factoring1.Services
{
    public interface IFactureService
    {
        Task<List<Facture>> GetFacturesByContratIdAsync(int contratId);
        Task<List<FactureWithCountDto>> GetFacturesByAcheteurAndContratIdAsync(int contratId, int acheteurId);

        Task<List<Facture>> GetFacturesByBorderau(int contratId, int bordereauId,int individuId );
    }
}
