using factoring1.Models;
using factoring1.FrameworkEtDrivers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using factoring1.DTO;

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
            return await _context.Bordereaux.Where(b => b.ContratId == contratId && b.Statut == Bordereau.StatusBordereau.Approuved).SumAsync(b => b.MontantTotal);

}
        public async Task<List<Bordereau>> GetBordereauxByContratAndIndividuAsync(int contratId, int individuId)
        {
            return await _context.Bordereaux
                .Where(b => b.ContratId == contratId && b.Contrat.IndividuContrats.Any(ic => ic.IndividuId == individuId))
                .ToListAsync();
        }
        public  async Task<Bordereau> GetBordereauWithFactures(int bordereauId){
           
                return await _context.Bordereaux.Where(b => b.BordereauId == bordereauId)
                    .Include(b => b.Factures).ThenInclude(f => f.Individu)
                    .FirstOrDefaultAsync();
            
        }
        public async Task<Bordereau> ValidateBordereauAsync(BordereauValidateCredencials credencials){
            var bordereau = await _context.Bordereaux.FirstOrDefaultAsync(b => b.BordereauId == credencials.BordereauId);
          if (bordereau != null)
            {
             if(credencials.Action=="refuse"){

            bordereau.Statut = Bordereau.StatusBordereau.Rejected;
          
           }
           if(credencials.Action=="accept"){
            bordereau.Statut = Bordereau.StatusBordereau.Approuved;
            }
            _context.Bordereaux.Update(bordereau);
            await _context.SaveChangesAsync();
            
          }
           return bordereau; 
        }

    }
}
