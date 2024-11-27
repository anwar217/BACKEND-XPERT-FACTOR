using factoring1.DTO;
using factoring1.Models;

namespace factoring1.Repositories
{
    public interface ILitigeRepository
    {
        Task<Litige> AddLitigeAsync(Litige litige);
        //Task<IEnumerable<Litige>> GetLitigesByFacture(int factureId, int individuId);
        Task<Litige> ValidateLitigeAsync(LitigeValidateCredencials credencials);
    }
}
