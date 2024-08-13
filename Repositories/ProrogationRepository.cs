using System.Threading.Tasks;
using factoring1.Models;
using factoring1.FrameworkEtDrivers;
using Microsoft.EntityFrameworkCore;

namespace factoring1.Repositories
{
    public class ProrogationRepository : IProrogationRepository
    {
        private readonly FactoringDbContext _context;

        public ProrogationRepository(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task<Prorogation> AddProrogationAsync(Prorogation prorogation)
        {
            _context.Prorogations.Add(prorogation);
            await _context.SaveChangesAsync();
            return prorogation;
        }

        

     
    }
}
