using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using factoring1.Models;
using factoring1.Services;
using System.Text.Json.Serialization;

namespace factoring1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancementController : ControllerBase
    {
        private readonly IFinancementService _financementService;

        public FinancementController(IFinancementService financementService)
        {
            _financementService = financementService;
        }

        [HttpPost("{individuId}/financements")]
        public async Task<IActionResult> AddFinancement(int individuId, [FromBody] Financement financement)
        {
            try
            {
                // Vérifier si TypeDeFinancement est valide
                if (financement.TypeDeFinancement != TypeDeFinancement.Financement &&
                    financement.TypeDeFinancement != TypeDeFinancement.LiberationFDG)
                {
                    return BadRequest("Le champ TypeDeFinancement doit être soit 'Financement' ou 'LiberationFDG'.");
                }

                var newFinancement = await _financementService.AddFinancement(individuId, financement);

                return Ok(newFinancement);
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
        [HttpGet("contrat/{contratId}")]
        public async Task<IActionResult> GetFinancementsByContratId(int contratId)
        {
            try
            {
                // Extraire l'IndividuId depuis le JWT de l'utilisateur connecté
                var individuIdClaim = User.FindFirst("id");
                if (individuIdClaim == null)
                {
                    return Unauthorized("Utilisateur non authentifié.");
                }

                int individuId = int.Parse(individuIdClaim.Value);

                // Récupérer les financements pour l'IndividuId et le ContratId spécifiés
                var financements = await _financementService.GetFinancementsByContratAndIndividuIdAsync(contratId, individuId);

                if (financements == null || financements.Count == 0)
                {
                    return NotFound($"Aucun financement trouvé pour le contrat ID {contratId} et l'utilisateur connecté.");
                }

                return Ok(financements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }
    }
}
