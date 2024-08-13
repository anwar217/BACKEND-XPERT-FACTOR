using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Repositories;

namespace factoring1.Services
{


    public class ProrogationService : IProrogationService
    {
        
        
            private readonly IProrogationRepository _prorogationRepository;

            public ProrogationService(IProrogationRepository prorogationRepository)
            {
                _prorogationRepository = prorogationRepository;
            }

            public async Task<Prorogation> AddProrogationAsync(int contratId, int factureId, Prorogation prorogation)
            {
              

                

                prorogation.ContratId = contratId;
                prorogation.FactureId = factureId;
                return await _prorogationRepository.AddProrogationAsync(prorogation);
            }
        }
    
}
