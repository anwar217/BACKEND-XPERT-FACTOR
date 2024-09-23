using factoring1.Models;
namespace factoring1.Services;
public interface IDisponibleService
{
    Task<List<Disponible>> GetDisponiblesByContratId(int contratId);
    Task<Disponible> AddDisponible(Disponible disponible);
}