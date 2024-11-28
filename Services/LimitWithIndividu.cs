using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using factoring1.DTO;
using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using Microsoft.EntityFrameworkCore;

namespace factoring1.Services
{
    public class LimitWithIndividu
    {
        private readonly FactoringDbContext _dbContext;

        public LimitWithIndividu(FactoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
public async Task<List<AcheteurWithPendingLimiteCount>> GetAdherentsWithLimiteCountByContratIdAsync(int contratId)
        {
            // Get the total limit count for the contract
            var totalLimitCount = await _dbContext.Limites
                .Where(l => l.ContratId == contratId&&l.Status==Limite.StatusLimit.Pending)
                .CountAsync();

            // Get adherents and attach the count
            var adherents = await _dbContext.IndividuContrats
                .Where(ic => ic.ContratId == contratId && ic.Role == IndividuContrat.RoleType.Acheteur)
                .Select(ic => new AcheteurWithPendingLimiteCount
                {
                    Acheteur = ic.Individu,
                   PendingLimiteCount = totalLimitCount // Use the pre-calculated count
                })
                .ToListAsync();

            return adherents;

        }
    } }
