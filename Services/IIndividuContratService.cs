using System.Threading.Tasks;

namespace factoring1.Services
{
    public interface IIndividuContratService
    {
        Task<bool> IsAdherent(int contratId);
    }
}
