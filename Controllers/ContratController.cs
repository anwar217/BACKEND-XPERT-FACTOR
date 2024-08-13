using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using factoring1.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace factoring1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class ContratController : ControllerBase
    {
        private readonly IContratService _contratService;

        public ContratController(IContratService contratService)
        {
            _contratService = contratService;
        }

        [HttpGet("adherents/contrats")]
        public async Task<IActionResult> GetContratsAdherents()
        {
            try
            {
                // Extract the IndividuId from the JWT token
                var individuIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("id");
                if (individuIdClaim == null)
                {
                    return Unauthorized("IndividuId not found in token.");
                }

                int individuId = int.Parse(individuIdClaim.Value);

                var contrats = await _contratService.GetContratsAdherentsByIndividuId(individuId);

                if (contrats.Count == 0)
                {
                    return BadRequest($"L'individu avec l'ID {individuId} n'est pas un adhérent pour tous les contrats.");
                }

                return Ok(contrats);
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
