using factoring1.Models;
using factoring1.Repositories;
using System.Threading.Tasks;

namespace factoring1.Services
{
    public class IndividuContratService : IIndividuContratService
    {
        private readonly IIndividuContratRepository _individuContratRepository;

        public IndividuContratService(IIndividuContratRepository individuContratRepository)
        {
            _individuContratRepository = individuContratRepository;
        }

        public async Task<bool> IsAdherent(int contratId)
        {
            var individuContrat = await _individuContratRepository.GetIndividuContratByContratId(contratId);
            return individuContrat?.Role == IndividuContrat.RoleType.Adherent;
        }
    }
}
