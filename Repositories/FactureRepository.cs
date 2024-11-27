using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using factoring1.Models;
using factoring1.FrameworkEtDrivers;
using factoring1.Repositories;
using factoring1.DTO;
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

        public async Task<List<FactureWithCountDto>> GetFacturesByAcheteurAndContratIdAsync(int contratId, int acheteurId)
        {
            // Check if the buyer has the specified contract
            var acheteurContrat = await _context.IndividuContrats
               .Where(ic => ic.ContratId == contratId && ic.IndividuId == acheteurId && ic.Role == IndividuContrat.RoleType.Acheteur)
               .FirstOrDefaultAsync();
            Console.WriteLine("hellloooooooo");
            if (acheteurContrat == null)
            {
                return new List<FactureWithCountDto>(); // Return empty list if not found
            }
            Console.WriteLine("hellloooooooo");

            // Retrieve invoices related to the contract and buyer
            var facturesWithLitigeCount = await _context.Factures
                .Where(f => f.ContratId == contratId && f.IndividuId == acheteurId)
                .Select(f => new FactureWithCountDto
                {
                    Facture = f,
                    LitigeCount = _context.Litiges.Count(l => l.FactureId == f.FactureId && l.Statut == Litige.StatusLitige.Pending),
                     ProrogationCount = _context.Prorogations.Count(l => l.FactureId == f.FactureId && l.Statut == Prorogation.StatusProrogation.Pending)

                })
                .ToListAsync();

            return facturesWithLitigeCount;
        }


        public async Task<List<Facture>> GetFacturesByBordereau(int contratId, int bordereauId, int individuId)
        {
            // Vérifier si l'individu possède bien le contrat spécifié
            var factures = await _context.Factures
          .Where(f => f.ContratId == contratId && f.BordereauId == bordereauId && f.IndividuId == individuId)
          .ToListAsync();

            return factures;
        }

        public async Task<decimal>GetFactureEnCoursByContratIdAsync(int contratId)
          {
             return await _context.Factures.Where(f => f.ContratId == contratId&& f.Status == Facture.FactureStatus.inProgress ).SumAsync(f => f.MontantDocument);
         }     

        public async Task<decimal> GetFacturesApprouvedByContratIdAsync(int contratId)
        {
            return await _context.Factures.Where(f => f.ContratId == contratId && (f.Status == Facture.FactureStatus.inProgress|| f.Status == Facture.FactureStatus.paid)).SumAsync(f => f.MontantDocument);
        }
        public async Task<Facture> GetFacturesWithLitigesAsync(int factureId)
        {
            return await _context.Factures.Include(f => f.Litiges).FirstOrDefaultAsync(f => f.FactureId == factureId);
        }
        public async Task<Facture> GetFacturesWithProrogationsAsync(int factureId)
        {
            return await _context.Factures.Include(f => f.Prorogations).FirstOrDefaultAsync(f => f.FactureId == factureId);
        }

    }
  

}
