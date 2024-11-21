using Microsoft.AspNetCore.Mvc;

using factoring1.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace factoring1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculController
    {
        private readonly CalculContrat _calculContrat;

        public CalculController(CalculContrat calculContrat) { 
            _calculContrat = calculContrat;
        }
        
        
    }
}
