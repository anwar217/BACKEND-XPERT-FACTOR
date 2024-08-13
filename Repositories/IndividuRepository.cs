using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using Microsoft.EntityFrameworkCore;

public class IndividuRepository : IIndividuRepository
{
    private readonly FactoringDbContext _context;

    public IndividuRepository(FactoringDbContext context)
    {
        _context = context;
    }

    public async Task<List<Individu>> GetAcheteursByContrat(int contratId,int adherentId)
    {
        return await _context.Individus
            .Where(i => i.IndividuContrats
                .Any(ic => ic.ContratId == contratId && ic.Role == IndividuContrat.RoleType.Acheteur))
            .ToListAsync();
    }

    public async Task UpdateRolesToAcheteurAsync(IEnumerable<int> individuIds)
    {
        var individus = await _context.IndividuContrats
            .Where(ic => individuIds.Contains(ic.IndividuId))
            .ToListAsync();

        foreach (var individuContrat in individus)
        {
            individuContrat.Role = IndividuContrat.RoleType.Acheteur;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Individu>> GetIndividusWithRoleIndividuAsync()
    {
        return await _context.Individus
            .Where(i => i.IndividuContrats.Any(ic => ic.Role != IndividuContrat.RoleType.Acheteur && ic.Role != IndividuContrat.RoleType.Adherent))
            .ToListAsync();
    }

    public async Task<Individu?> GetAdherentProfileAsync(int individuId)
    {
        return await _context.Individus
            .Include(i => i.IndividuContrats)
            .Include(i => i.Factures)
            .FirstOrDefaultAsync(i => i.IndividuId == individuId &&
                i.IndividuContrats.Any(ic => ic.Role == IndividuContrat.RoleType.Adherent));
    }

    public async Task<bool> UpdateAdherentProfileAsync(Individu individu)
    {
        _context.Individus.Update(individu);
        return await _context.SaveChangesAsync() > 0;
    }


}
