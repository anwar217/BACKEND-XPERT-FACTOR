using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using Microsoft.EntityFrameworkCore;

namespace factoring1.Services
{
    public class CalculContrat
    {
        private readonly FactoringDbContext _context;
        public CalculContrat(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> CalculerMontantTotalApprouveAsync(int contratId)
        {
            var totalMontantApprouve = await _context.Bordereaux
                .Where(b => b.ContratId == contratId && b.Statut == Bordereau.StatusBordereau.Approuved)
                .SumAsync(b => b.MontantTotal);

            return totalMontantApprouve;
        }
    }

}
