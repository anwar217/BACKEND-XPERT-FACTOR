using factoring1.Models;
using factoring1.FrameworkEtDrivers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace factoring1.Repositories
{
    public class BordereauRepository : IBordereauRepository
    {
        private readonly FactoringDbContext _context;

        public BordereauRepository(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task<Bordereau> AddBordereauAsync(Bordereau bordereau)
        {
            await _context.Bordereaux.AddAsync(bordereau);
            await _context.SaveChangesAsync();
            return bordereau;
        }
        public async Task<decimal>GetBordereauApprouvedSumByContratIdAsync(int contratId){
            return await _context.Bordereaux.Where(b => b.ContratId == contratId && b. == Bordereau.StatusBordereau.approuved).SumAsync(b => b.MontantTotal);
        }
    }
}
