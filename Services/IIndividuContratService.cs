using System.Threading.Tasks;

namespace factoring1.Services
{
    public interface IIndividuContratService
    {
        Task<bool> IsAdherent(int contratId);
        Task<bool> AjouterAcheteurAuContrat(int adherentId, int contratId, int acheteurId);

    }
}
