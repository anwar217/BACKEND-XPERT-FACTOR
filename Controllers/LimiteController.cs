using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using factoring1.DTO;
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
        private readonly LimitWithIndividu _limitWithIndividuService;
        public LimiteController(ILimiteService limiteService,LimitWithIndividu limitWithIndividu)
        {
            _limiteService = limiteService;
            _limitWithIndividuService= limitWithIndividu;

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

            var limites = await _limiteService.GetLimitesByContratIdAsync(contratId, individuId);
            if (limites == null)
            {
                return NotFound($"Aucun limite trouvé pour le contrat ID {contratId} et l'utilisateur connecté.");
            }
            return Ok(limites);
        }
        [HttpGet("acheteur/{contratId}")]
        public async Task<ActionResult<List<AcheteurWithPendingLimiteCount>>> GetAcheteursWithPendingLimitsByContratId(int contratId)
        {
            var acheteursWithPendingLimits = await _limitWithIndividuService.GetAdherentsWithLimiteCountByContratIdAsync(contratId);

            if (acheteursWithPendingLimits == null || acheteursWithPendingLimits.Count == 0)
            {
                return NotFound("No acheteurs found for the specified contrat or no pending limits.");
            }

            return Ok(acheteursWithPendingLimits);
        }
        [HttpPost("admin/validate")]
        public async Task<IActionResult> ValidateLimite([FromBody] LimiteValidateCredencials credencials)
        {
            try
            {
                await _limiteService.ValidateLimiteAsync(credencials);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }
    }
}
