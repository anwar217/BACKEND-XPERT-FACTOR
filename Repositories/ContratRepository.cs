using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using factoring1.Models;
using factoring1.FrameworkEtDrivers;

namespace factoring1.Repositories
{
    public class ContratRepository : IContratRepository
    {
        private readonly FactoringDbContext _context;

        public ContratRepository(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contrat>> GetContratsByIndividuId(int individuId)
        {
            return await _context.Contrats
                .Include(c => c.IndividuContrats)
                .Where(c => c.IndividuContrats.Any(ic => ic.IndividuId == individuId && ic.Role == IndividuContrat.RoleType.Adherent))
                .ToListAsync();
        }

        public async Task<Contrat> GetContratByIdAndIndividuId(int contratId, int individuId)
        {
            return await _context.Contrats
                .Include(c => c.IndividuContrats)
                .SingleOrDefaultAsync(c => c.ContratId == contratId &&
                                          c.IndividuContrats.Any(ic => ic.IndividuId == individuId));
        }

        public async Task<bool> IsIndividuAdherentForContrat(int individuId, int contratId)
        {
            return await _context.IndividuContrats
                .AnyAsync(ic => ic.IndividuId == individuId && ic.ContratId == contratId && ic.Role == IndividuContrat.RoleType.Adherent);
        }
    }
}
