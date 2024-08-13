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
    }
}
