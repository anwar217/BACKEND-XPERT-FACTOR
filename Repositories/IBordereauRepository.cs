using factoring1.Models;
using System.Threading.Tasks;

namespace factoring1.Repositories
{
    public interface IBordereauRepository
    {
        Task<Bordereau> AddBordereauAsync(Bordereau bordereau);
    }
}
