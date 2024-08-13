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
    }
   }

