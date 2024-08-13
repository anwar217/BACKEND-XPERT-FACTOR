using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Services;
using Microsoft.AspNetCore.Mvc;

namespace factoring1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProrogationController : ControllerBase
    {
        private readonly IProrogationService _prorogationService;

        public ProrogationController(IProrogationService prorogationService)
        {
            _prorogationService = prorogationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProrogation(int contratId, int factureId, Prorogation prorogation)
        {
            try
            {
                var newProrogation = await _prorogationService.AddProrogationAsync(contratId, factureId, prorogation);
                return Ok(newProrogation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
