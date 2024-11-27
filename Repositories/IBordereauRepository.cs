using factoring1.DTO;
using factoring1.Models;
using System.Threading.Tasks;

namespace factoring1.Repositories
{
    public interface IBordereauRepository
    {
        Task<Bordereau> AddBordereauAsync(Bordereau bordereau);
        Task<List<Bordereau>> GetBordereauxByContratAndIndividuAsync(int contratId, int individuId);
     Task<decimal>GetBordereauApprouvedSumByContratIdAsync(int contratId);
        Task<Bordereau> GetBordereauWithFactures(int bordereauId);
        Task<Bordereau> ValidateBordereauAsync(BordereauValidateCredencials credencials);
    }
}
