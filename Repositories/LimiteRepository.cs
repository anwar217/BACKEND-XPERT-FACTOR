using System.Threading.Tasks;
using factoring1.Models;
using factoring1.FrameworkEtDrivers;
using Microsoft.EntityFrameworkCore;
using factoring1.DTO;

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
        public async Task<IEnumerable<Limite>> GetLimitesByContratIdAsync(int contratId,int individuId)
        {
            return await _context.Limites
                                 .Where(b => b.ContratId == contratId &&
                                 b.Contrat.IndividuContrats.Any(ic => ic.IndividuId == individuId))
                .ToListAsync();
        }

        public async Task<decimal> GetLimitApprouvedSumByContratIdAsync(int contratId)
        {
            return await _context.Limites.Where(b => b.ContratId == contratId && b.Status == Limite.StatusLimit.Approuved).SumAsync(b => b.LimiteFinancement);
        }
        public async Task<Limite> ValidateLimiteAsync(LimiteValidateCredencials credencials){
            var limite= _context.Limites.FirstOrDefault(b => b.Id == credencials.LimiteId);
            if (limite != null){
                if(credencials.Action=="refuse"){
                    limite.Status = Limite.StatusLimit.Rejected;
                }
                if(credencials.Action=="accept"){
                    limite.Status = Limite.StatusLimit.Approuved;   
                }
            }
            _context.Limites.Update(limite);
            await _context.SaveChangesAsync();
            return limite;
        }
    }
}
