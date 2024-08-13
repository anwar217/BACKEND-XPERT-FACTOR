using factoring1.Models;
using System.Threading.Tasks;

namespace factoring1.Services
{
    public interface IBordereauService
    {
        Task<Bordereau> AddBordereauAsync(Bordereau bordereau);
    }
}
