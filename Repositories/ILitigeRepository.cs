using factoring1.Models;

namespace factoring1.Repositories
{
    public interface ILitigeRepository
    {
        Task<Litige> AddLitigeAsync(Litige litige);
        //Task<IEnumerable<Litige>> GetLitigesByFacture(int factureId, int individuId);

    }
}
