using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static factoring1.Models.IndividuContrat;

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

        public async Task<bool> EstProprietaireDuContrat(int adherentId, int contratId)
        {
            return await _context.IndividuContrats
                .AnyAsync(ic => ic.IndividuId == adherentId && ic.ContratId == contratId && ic.Role == RoleType.Adherent);
        }

        // Vérifier si l'acheteur est déjà présent pour ce contrat
        public async Task<bool> AcheteurDejaPresent(int contratId, int acheteurId)
        {
            return await _context.IndividuContrats
                .AnyAsync(ic => ic.ContratId == contratId && ic.IndividuId == acheteurId && ic.Role == RoleType.Acheteur);
        }

        // Ajouter un individu avec un rôle dans un contrat
        public async Task AjouterIndividuContrat(IndividuContrat individuContrat)
        {
            _context.IndividuContrats.Add(individuContrat);
            await _context.SaveChangesAsync();
        }
    }
}
