using factoring1.DTO;
using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using factoring1.Repositories;
using Microsoft.EntityFrameworkCore;

namespace factoring1.Services
{
    public class LitigeService : ILitigeService
    {
        private readonly FactoringDbContext _context;
        private readonly ILitigeRepository _litigeRepository;

        public LitigeService(FactoringDbContext context, ILitigeRepository litigeRepository)
        {
            _context = context;
            _litigeRepository = litigeRepository;
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


        public async Task<IEnumerable<Litige>> GetLitigesByFacture(int factureId)
        {
            var litiges = await _context.Litiges
                .Where(l => l.FactureId == factureId )
                .ToListAsync();

            return litiges;
        }
        public async Task<Litige>ValidateLitigeAsync(LitigeValidateCredencials credencials){
            var litige = await _litigeRepository.ValidateLitigeAsync(credencials) ?? throw new ArgumentException("Le litige n'existe pas.");
            return litige;
        }

    }
}
