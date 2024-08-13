using factoring1.FrameworkEtDrivers;
using factoring1.Models;

namespace factoring1.Services
{
    public class LitigeService : ILitigeService
    {
        private readonly FactoringDbContext _context;

        public LitigeService(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task<Litige> AddLitigeAsync(int contratId, int factureId, Litige litige)
        {
            var contrat = await _context.Contrats.FindAsync(contratId);
            if (contrat == null)
            {
                throw new ArgumentException("Contrat not found");
            }

            var facture = await _context.Factures.FindAsync(factureId);
            if (facture == null)
            {
                throw new ArgumentException("Facture not found");
            }

            litige.ContratId = contratId;
            litige.FactureId = factureId;
            _context.Litiges.Add(litige);
            await _context.SaveChangesAsync();

            return litige;
        }
    }
}
