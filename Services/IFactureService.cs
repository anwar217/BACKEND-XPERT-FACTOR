using factoring1.DTO;
using factoring1.Models;

namespace factoring1.Services
{
    public interface IFactureService
    {
        Task<List<Facture>> GetFacturesByContratIdAsync(int contratId);
        Task<List<FactureWithCountDto>> GetFacturesByAcheteurAndContratIdAsync(int contratId, int acheteurId);

        Task<List<Facture>> GetFacturesByBorderau(int contratId, int bordereauId,int individuId );
       Task<decimal> GetFactureEnCoursByContratIdAsync(int contratId);
       Task<decimal>GetFacturesApprouvedByContratIdAsync(int contratId);
        Task<Facture> GetFacturesWithLitigesAsync(int factureId);
        Task<Facture> GetFacturesWithProrogationsAsync(int factureId);
    }
}
