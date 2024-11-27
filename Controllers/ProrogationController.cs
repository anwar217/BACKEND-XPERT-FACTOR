using System.Threading.Tasks;
using factoring1.DTO;
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

        [HttpGet("facture/{factureId}")]
        public async Task<IActionResult> GetProrogationByFacture(int factureId)
        {
            var individuIdClaim = User.FindFirst("id");
            if (individuIdClaim == null)
            {
                return Unauthorized("IndividuId not found in token.");
            }

            int individuId = int.Parse(individuIdClaim.Value);

            var limites = await _prorogationService.GetProrogationByFacture(factureId);
            if (limites == null)
            {
                return NotFound($"Aucun litige trouvé pour la facture ID {factureId} et l'utilisateur connecté.");
            }
            return Ok(limites);
        }
        [HttpPost("Admin/validate")]
        public async Task<IActionResult> ValidateProrogation(ProrogationValidatiteCredencials credencials)
        {
            try
            {
                var prorogation = await _prorogationService.ValidateProrogationAsync(credencials);
                return Ok(prorogation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
