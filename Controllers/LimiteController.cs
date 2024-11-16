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

        [HttpGet("contrat/{contratId}")]
        public async Task<IActionResult> GetLimitesByContratId(int contratId)
        {
            var individuIdClaim = User.FindFirst("id");
            if (individuIdClaim == null)
            {
                return Unauthorized("IndividuId not found in token.");
            }

            int individuId = int.Parse(individuIdClaim.Value);

            var limites = await _limiteService.GetLimitesByContratIdAsync(contratId,individuId);
            if (limites == null )
            {
                return NotFound($"Aucun limite trouvé pour le contrat ID {contratId} et l'utilisateur connecté.");
            }
            return Ok(limites);
        }

    }
}
