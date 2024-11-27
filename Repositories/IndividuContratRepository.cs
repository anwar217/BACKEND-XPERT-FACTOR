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
            // Vérifier si l'adhérent est bien le propriétaire du contrat
            return await _context.IndividuContrats
                .AnyAsync(ic => ic.ContratId == contratId && ic.IndividuId == adherentId && ic.Role == RoleType.Adherent);
        }

        public async Task<bool> AcheteurDejaPresent(int contratId, int acheteurId)
        {
            // Vérifier si l'acheteur est déjà assigné au contrat
            return await _context.IndividuContrats
                .AnyAsync(ic => ic.ContratId == contratId && ic.IndividuId == acheteurId && ic.Role == RoleType.Acheteur);
        }

        public async Task AjouterIndividuContrat(IndividuContrat individuContrat)
        {
            // Ajouter un nouvel acheteur au contrat
            _context.IndividuContrats.Add(individuContrat);
            await _context.SaveChangesAsync();
        }
        public async Task LinkContractToAdherent(int contractId, int adherentId){
            var  individuContrat =new IndividuContrat {IndividuId= adherentId, ContratId= contractId, Role = RoleType.Adherent };
             _context.IndividuContrats.Add(individuContrat);
            await _context.SaveChangesAsync();
        }
    }
}
