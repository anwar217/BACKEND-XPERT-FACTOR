
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using Microsoft.EntityFrameworkCore;


namespace factoring1.Services
{
public class DisponibleService : IDisponibleService
{
    private readonly FactoringDbContext _context;

    public DisponibleService(FactoringDbContext context)
    {
        _context = context;
    }

    public async Task<List<Disponible>> GetDisponiblesByContratId(int contratId)
    {
        return await _context.Disponibles
            .Where(d => d.ContratId == contratId)
            .ToListAsync();
    }

    public async Task<Disponible> AddDisponible(Disponible disponible)
    {
        _context.Disponibles.Add(disponible);
        await _context.SaveChangesAsync();
        return disponible;
    }
}
}