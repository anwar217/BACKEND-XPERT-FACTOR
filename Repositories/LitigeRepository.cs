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

       


    }
}
