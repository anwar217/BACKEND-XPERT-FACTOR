using System.Threading.Tasks;
using factoring1.DTO;

namespace factoring1.Services
{
    public interface IIndividuContratService
    {
        Task<bool> IsAdherent(int contratId);
        Task<bool> AjouterAcheteursAuContrat(int adherentId, int contratId, List<int> acheteurIds);
        


    }
}
