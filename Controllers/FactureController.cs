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
        var emptyFacture = Array.Empty<Facture>();
        var factures = await _factureService.GetFacturesByAcheteurAndContratIdAsync(contratId, acheteurId);

        if (factures == null || !factures.Any())
        {
            return Ok(emptyFacture);
        }

        return Ok(factures);
    }

    [HttpGet("{contratId}/{bordereauId}")]
    public async Task<IActionResult> GetFacturesByContratBordereauIndividu(int contratId, int bordereauId)
    {
        // Obtenir l'ID de l'individu connecté depuis le token JWT
        var individuIdClaim = User.FindFirst("id");
        if (individuIdClaim == null)
        {
            return Unauthorized("IndividuId non trouvé dans le token.");
        }

        int individuId = int.Parse(individuIdClaim.Value);

        // Appeler le service pour récupérer les factures
        var factures = await _factureService.GetFacturesByBorderau(contratId, bordereauId, individuId);

        // Vérifier si des factures ont été trouvées
        if (factures == null || !factures.Any())
        {
            return NotFound("Aucune facture trouvée pour ce contrat, bordereau et individu.");
        }

        // Retourner les factures
        return Ok(factures);
    }

}
