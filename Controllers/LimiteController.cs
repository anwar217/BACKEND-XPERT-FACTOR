using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Services;
using Microsoft.AspNetCore.Mvc;

namespace factoring1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LimiteController : ControllerBase
    {
        private readonly ILimiteService _limiteService;

        public LimiteController(ILimiteService limiteService)
        {
            _limiteService = limiteService;
        }

        [HttpPost("{contratId}")]
        public async Task<IActionResult> AddLimite(int contratId, [FromBody] Limite limite)
        {
            try
            {
                var newLimite = await _limiteService.AddLimiteAsync(contratId, limite);
                return Ok(newLimite);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{contratId}/limites")]
        public async Task<IActionResult> GetLimitesByContratId(int contratId)
        {
            var limites = await _limiteService.GetLimitesByContratIdAsync(contratId);
            return Ok(limites);
        }

    }
}
