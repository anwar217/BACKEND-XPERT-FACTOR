using factoring1.Models;

namespace factoring1.Repositories
{
    public interface IProrogationRepository
    {
        Task<Prorogation> AddProrogationAsync(Prorogation prorogation);

    }
}
