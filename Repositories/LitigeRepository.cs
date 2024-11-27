using factoring1.DTO;
using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using Microsoft.EntityFrameworkCore;

namespace factoring1.Repositories
{
    public class LitigeRepository : ILitigeRepository
    {
        private readonly FactoringDbContext _context;

        public LitigeRepository(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task<Litige> AddLitigeAsync(Litige litige)
        {
            _context.Litiges.Add(litige);
            await _context.SaveChangesAsync();
            return litige;
        }

       
    public async Task<Litige> ValidateLitigeAsync(LitigeValidateCredencials credencials)
        {
                var litige = await _context.Litiges.FirstOrDefaultAsync(l => l.LitigeId == credencials.LitigeId );
          if (litige != null)
            {
             if(credencials.Action=="refuse"){

            litige.Statut = Litige.StatusLitige.Rejected;
          
           }
           if(credencials.Action=="accept"){
            litige.Statut = Litige.StatusLitige.Approuved;
            }
            _context.Litiges.Update(litige);
            await _context.SaveChangesAsync();
            
          }
           return litige; 

    }
}
}
