using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using factoring1.DTO;
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

   public async Task<IEnumerable<Individu>> GetIndividusWithRoleIndividuAsync(int contratId)
{
  return await _context.Individus
        .Where(i => !i.IndividuContrats.Any(
            ic => ic.ContratId == contratId &&
            (ic.Role == IndividuContrat.RoleType.Acheteur ||
             ic.Role == IndividuContrat.RoleType.Adherent)))
        .ToListAsync();
}

    public async Task<Individu?> GetAdherentProfileAsync(int individuId)
    {
        return await _context.Individus
            .Include(i => i.IndividuContrats)
            .Include(i => i.Factures)
            .FirstOrDefaultAsync(i => i.IndividuId == individuId &&
                i.IndividuContrats.Any(
                    ic => ic.Role == IndividuContrat.RoleType.Adherent));
    }

    public async Task<bool> UpdateAdherentProfileAsync(Individu individu)
    {
        _context.Individus.Update(individu);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<Individu> AddIndividuAsync(Individu individu)
    {
        // Ajouter l'individu à la base de données
        _context.Individus.Add(individu);
        await _context.SaveChangesAsync(); // Sauvegarder les modifications

        // Retourner l'individu créé
        return individu;
    }
    public async Task<Individu> GetIndividuByIdAsync(int individuId)
    {
        return await _context.Individus
                             .FirstOrDefaultAsync(i => i.IndividuId == individuId);
    }
    public async Task<List<Individu>> GetAllIndividus()
    {
        return await _context.Individus
         
                   
            .ToListAsync();
    }


   public async Task<List<AdherentContratMontantCount>> GetAllAdherents(){
  
return await _context.IndividuContrats
    .Where(ic => ic.Role == IndividuContrat.RoleType.Adherent)
    .GroupBy(ic => ic.Individu)
    .Select(g => new AdherentContratMontantCount
    {
        Adherent = g.Key,
        ContratMontantCount = g.Sum(ic => ic.Contrat.MontantContrat),
    })
    .ToListAsync();
    } 
    public async Task<List<AcheteurFactureSumWithStatus>> GetAllAcheteurs(){
  
return await _context.IndividuContrats
    .Where(ic => ic.Role == IndividuContrat.RoleType.Acheteur)
    .GroupBy(ic => ic.Individu)
    .Select(g => new AcheteurFactureSumWithStatus
    {
        Acheteur = g.Key,
        FacturePaidSum = g.Sum(ic => ic.Individu.Factures.Where(f => f.Status == Facture.FactureStatus.paid).Sum(f => f.MontantDocument)),
        FactureInProgressSum = g.Sum(ic => ic.Individu.Factures.Where(f => f.Status == Facture.FactureStatus.inProgress).Sum(f => f.MontantDocument)),
    })
    .ToListAsync();
    }
}

