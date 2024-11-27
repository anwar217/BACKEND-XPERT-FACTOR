using factoring1.DTO;
using factoring1.Models;

namespace factoring1.Services
{
    public interface IProrogationService
    {
        Task<Prorogation> AddProrogationAsync(int contratId, int factureId, Prorogation prorogation);
        Task<IEnumerable<Prorogation>> GetProrogationByFacture(int factureId);
        Task <Prorogation> ValidateProrogationAsync(ProrogationValidatiteCredencials credencials);
    }
}
