﻿using System.Threading.Tasks;
using factoring1.Models;
using factoring1.FrameworkEtDrivers;

namespace factoring1.Repositories
{
    public class FinancementRepository : IFinancementRepository
    {
        private readonly FactoringDbContext _context;

        public FinancementRepository(FactoringDbContext context)
        {
            _context = context;
        }

        public async Task Add(Financement financement)
        {
            _context.Financements.Add(financement);
            await _context.SaveChangesAsync();
        }
    }
}
