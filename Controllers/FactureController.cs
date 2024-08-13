using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Ensure that the user is authenticated
public class FactureController : ControllerBase
{
    private readonly IFactureService _factureService;
    private readonly IContratService _contratService;

    public FactureController(IFactureService factureService, IContratService contratService)
    {
        _factureService = factureService;
        _contratService = contratService;
    }

    private int GetCurrentIndividuId()
    {
        // Extract the individuId from the JWT token
        var individuIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier); // Adjust this claim type if needed
        if (int.TryParse(individuIdClaim, out int individuId))
        {
            return individuId;
        }
        throw new UnauthorizedAccessException("IndividuId not found in the JWT token.");
    }

    [HttpGet("GetFactures/{contratId}")]
    public async Task<IActionResult> GetFactures(int contratId)
    {
        try
        {
            int individuId = GetCurrentIndividuId(); // Get the current individuId from the token

            // Check if the contract belongs to the current adherent
           

            var factures = await _factureService.GetFacturesByContratIdAsync(contratId);

            if (factures == null || !factures.Any())
            {
                return NotFound($"Aucune facture trouvée pour le contrat avec l'ID {contratId}.");
            }

            return Ok(factures);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
        }
    }
}
