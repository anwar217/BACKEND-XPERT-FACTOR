using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace factoring1.Repositories
{
    public class IndividuContratRepository : IIndividuContratRepository
    {
        private readonly FactoringDbContext _context;

        public IndividuContratRepository(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task<IndividuContrat> GetIndividuContratByContratId(int contratId)
        {
            return await _context.IndividuContrats
                .FirstOrDefaultAsync(ic => ic.ContratId == contratId);
        }
    }
}
