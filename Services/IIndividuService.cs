using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.Models;

public interface IIndividuService
{
    Task<List<Individu>> GetAcheteursByContrat(int contratId,int adherentId);
    Task UpdateRolesToAcheteurAsync(IEnumerable<int> individuIds);
    Task<IEnumerable<Individu>> GetIndividusWithRoleIndividuAsync();
    Task<Individu?> GetAdherentProfileAsync(int individuId);
    Task<bool> UpdateAdherentProfileAsync(Individu individu);



}
