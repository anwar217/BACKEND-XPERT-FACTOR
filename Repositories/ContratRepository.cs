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

        public async Task<Contrat> AddContratForIndividuAsync(int contratId, int individuId)
        {
            // Vérifier si le contrat existe
            var existingContrat = await _context.Contrats
                .FirstOrDefaultAsync(c => c.ContratId == contratId);
            if (existingContrat == null)
            {
                throw new ArgumentException("Le contrat n'existe pas.");
            }

            // Créer une association entre l'adhérent et le contrat
            var individuContrat = new IndividuContrat
            {
                IndividuId = individuId,
                ContratId = contratId,
                Role = IndividuContrat.RoleType.Adherent
            };

            // Ajouter l'association à la base de données
            _context.IndividuContrats.Add(individuContrat);
            await _context.SaveChangesAsync();

            return existingContrat; // Retourner le contrat existant
        }

        public async Task<Contrat> GetContratByIdAsync(int contratId)
        {
            return await _context.Contrats.Where(c => c.ContratId == contratId).FirstOrDefaultAsync();
        }
        public async Task<List<Contrat>> GetAllContratsAsync()
        {
            return await _context.Contrats.ToListAsync();
               
           
             
        }
        public async Task<Contrat> CreateContratAsync(Contrat contrat){
          await  _context.Contrats.AddAsync(contrat);
         var result =   await _context.SaveChangesAsync();
            return contrat;
        }

        public async Task<Contrat>GetContratAdminByIdAsync(int contratId)
        {
            return await _context.Contrats
 
    .Where(c => c.ContratId == contratId)
    .Include(c => c.Bordereaux)
    .Include(c => c.Financements)
    .Include(c => c.Limites)
    .Include(c => c.IndividuContrats.
    Where(ic => ic.Role == IndividuContrat.RoleType.Adherent)).ThenInclude(ic => ic.Individu)
    .FirstOrDefaultAsync();
        }
    }
}

