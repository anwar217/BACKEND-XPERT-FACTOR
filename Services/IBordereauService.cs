using factoring1.DTO;
using factoring1.Models;
using System.Threading.Tasks;

namespace factoring1.Services
{
    public interface IBordereauService
    {
        Task<Bordereau> AddBordereauAsync(Bordereau bordereau);
        Task<List<Bordereau>> GetBordereauxByContratAndIndividuAsync(int contratId, int individuId);
        Task<decimal> GetBordereauApprouvedSumByContratIdAsync(int contratId);
        Task <Bordereau> GetBordereauxWithFactures(int bordereauId);
        Task ValidateBordereauAsync(BordereauValidateCredencials credencials);
    }
}
