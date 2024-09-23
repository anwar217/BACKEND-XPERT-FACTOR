using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Services;
namespace factoring1.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DisponibleController(IDisponibleService disponibleService) : ControllerBase
{
    private readonly IDisponibleService _disponibleService = disponibleService;

    [HttpGet("{contratId}")]
    public async Task<ActionResult<List<Disponible>>> GetDisponiblesByContratId(int contratId)
    {
        var disponibles = await _disponibleService.GetDisponiblesByContratId(contratId);
        if (disponibles == null || disponibles.Count == 0)
        {
            return NotFound("Aucun détail disponible trouvé pour ce contrat.");
        }

        return Ok(disponibles);
    }

    [HttpPost]
    public async Task<ActionResult<Disponible>> AddDisponible(Disponible disponible)
    {
        var addedDisponible = await _disponibleService.AddDisponible(disponible);
        return CreatedAtAction(nameof(GetDisponiblesByContratId), new { contratId = addedDisponible.ContratId }, addedDisponible);
    }
}