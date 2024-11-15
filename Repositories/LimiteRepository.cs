using System.Threading.Tasks;
using factoring1.Models;
using factoring1.FrameworkEtDrivers;
using Microsoft.EntityFrameworkCore;

namespace factoring1.Repositories
{
    public class LimiteRepository : ILimiteRepository
    {
        private readonly FactoringDbContext _context;

        public LimiteRepository(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task<Limite> AddLimiteAsync(Limite limite)
        {
            _context.Limites.Add(limite);
            await _context.SaveChangesAsync();
            return limite;
        }
        public async Task<Contrat> GetContratByIdAsync(int contratId)
        {
            return await _context.Contrats.FindAsync(contratId);
        }
        public async Task<IEnumerable<Limite>> GetLimitesByContratIdAsync(int contratId)
        {
            return await _context.Limites
                                 .Where(l => l.ContratId == contratId)
                                 .ToListAsync();
        }
    }
}
