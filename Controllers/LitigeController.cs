using AutoMapper.Features;
using factoring1.Models;
using factoring1.Services;
using Microsoft.AspNetCore.Mvc;

namespace factoring1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LitigeController : ControllerBase
    {
        private readonly ILitigeService _litigeService;

        public LitigeController(ILitigeService litigeService)
        {
            _litigeService = litigeService;
        }

        [HttpPost("{contratId}/factures/{factureId}/litiges")]
        public async Task<IActionResult> AddLitige(int contratId, int factureId, [FromBody] Litige litige)
        {
            try
            {
                var newLitige = await _litigeService.AddLitigeAsync(contratId, factureId, litige);
                return Ok(newLitige);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }


        [HttpGet("facture/{factureId}")]
        public async Task<IActionResult> GetLitigeByFactureId(int factureId)
        {
            var individuIdClaim = User.FindFirst("id");
            if (individuIdClaim == null)
            {
                return Unauthorized("IndividuId not found in token.");
            }

            int individuId = int.Parse(individuIdClaim.Value);

            var limites = await _litigeService.GetLitigesByFacture(factureId);
            if (limites == null )
            {
                return NotFound($"Aucun litige trouvé pour la facture ID {factureId} et l'utilisateur connecté.");
            }
            return Ok(limites);
        }
    }
   }

