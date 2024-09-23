using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Repositories;
namespace factoring1.Services;
public class IndividuService : IIndividuService
{
    private readonly IIndividuRepository _individuRepository;

    public IndividuService(IIndividuRepository individuRepository)
    {
        _individuRepository = individuRepository;
    }

    public async Task<List<Individu>> GetAcheteursByContrat(int contratId, int adherentId)
    {
        
        return await _individuRepository.GetAcheteursByContrat(contratId, adherentId);
    }

    public async Task UpdateRolesToAcheteurAsync(IEnumerable<int> individuIds)
    {
        await _individuRepository.UpdateRolesToAcheteurAsync(individuIds);
    }

    public async Task<IEnumerable<Individu>> GetIndividusWithRoleIndividuAsync(int contratId)
    {
        return await _individuRepository.GetIndividusWithRoleIndividuAsync(contratId);
    }

    public async Task<Individu?> GetAdherentProfileAsync(int individuId)
    {
        return await _individuRepository.GetAdherentProfileAsync(individuId);
    }

    public async Task<bool> UpdateAdherentProfileAsync(Individu individu)
    {
        return await _individuRepository.UpdateAdherentProfileAsync(individu);
    }

}
