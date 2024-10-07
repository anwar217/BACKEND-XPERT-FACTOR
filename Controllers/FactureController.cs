using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Services; // Assurez-vous que le bon espace de noms est utilisé

[ApiController]
[Route("api/[controller]")]
public class FactureController : ControllerBase
{
    private readonly IFactureService _factureService;

    public FactureController(IFactureService factureService)
    {
        _factureService = factureService;
    }

    [HttpGet("GetFactures/{contratId}")]
    public async Task<IActionResult> GetFactures(int contratId)
    {
        var factures = await _factureService.GetFacturesByContratIdAsync(contratId);

        if (factures == null || !factures.Any())
        {
            return NotFound();
        }

        return Ok(factures);

    }
    [HttpGet("GetFacturesByAcheteur/{contratId}/{acheteurId}")]
    public async Task<IActionResult> GetFacturesByAcheteur(int contratId, int acheteurId)
    {
        var factures = await _factureService.GetFacturesByAcheteurAndContratIdAsync(contratId, acheteurId);

        if (factures == null || !factures.Any())
        {
            return NotFound();
        }

        return Ok(factures);
    }
}
