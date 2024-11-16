using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.Models;
namespace factoring1.Services;
public interface IIndividuService
{
    Task<List<Individu>> GetAcheteursByContrat(int contratId,int adherentId);
    Task UpdateRolesToAcheteurAsync(IEnumerable<int> individuIds);
    Task<IEnumerable<Individu>> GetIndividusWithRoleIndividuAsync(int contratId);
    Task<Individu?> GetAdherentProfileAsync(int individuId);
    Task<bool> UpdateAdherentProfileAsync(Individu individu);
    Task<Individu> CreateIndividuAsync(Individu individu);



}
