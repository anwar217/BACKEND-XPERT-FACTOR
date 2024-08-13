using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using factoring1.Models;
using factoring1.FrameworkEtDrivers;
using factoring1.Repositories;
namespace factoring1.Services
{
    public class FactureRepository : IFactureRepository
    {
        private readonly FactoringDbContext _context;

        public FactureRepository(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task<List<Facture>> GetFacturesByContratIdAsync(int contratId)
        {
            return await _context.Factures
                .Where(f => f.ContratId == contratId)
                .ToListAsync();
        }
    }
}
