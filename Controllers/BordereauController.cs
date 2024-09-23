using factoring1.Models;
using factoring1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace factoring1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BordereauController : ControllerBase
    {
        private readonly IBordereauService _bordereauService;
        private readonly IIndividuContratService _individuContratService;

        public BordereauController(IBordereauService bordereauService, IIndividuContratService individuContratService)
        {
            _bordereauService = bordereauService;
            _individuContratService = individuContratService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBordereau([FromBody] Bordereau bordereau)
        {

            Console.WriteLine("bordereau");
            Console.WriteLine(bordereau.ToString());
            // Vérifier que le montant total du bordereau est égal à la somme des montants des factures
            var totalFactures = bordereau.Factures.Sum(f => f.MontantDocument);
            Console.WriteLine("totalFactures");
             Console.WriteLine(totalFactures);
            if (bordereau.MontantTotal != Math.Floor(totalFactures) )
            {
                return BadRequest("Le montant total du bordereau doit être égal à la somme des montants des factures.");
            }

            // Vérifier que le nombre de documents est égal au nombre de factures
            if (bordereau.NombreDocuments != bordereau.Factures.Count)
            {
                return BadRequest("Le nombre de documents doit être égal au nombre de factures.");
            }

            // Vérifier que chaque facture est liée à un individu dont le rôle est Adherent
            foreach (var facture in bordereau.Factures)
            {
                var isAdherent = await _individuContratService.IsAdherent(bordereau.ContratId);
                if (!isAdherent)
                {
                    return BadRequest($"La facture {facture.RefFacture} n'est pas liée à un individu dont le rôle est Adherent.");
                }
            }

            try
            {
                await _bordereauService.AddBordereauAsync(bordereau);
                return Ok(bordereau);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
