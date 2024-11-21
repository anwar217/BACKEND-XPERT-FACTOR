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
        private readonly CalculContrat _calculContrat;
        private readonly IFactureService _factureService;
        private readonly IBordereauService _bordereauService;
        private readonly ILimiteService _limiteService;

        public ContratController(IContratService contratService,CalculContrat calculContrat,IFactureService factureService,IBordereauService bordereauService,ILimiteService limiteService)
        {
            _contratService = contratService;
            _calculContrat = calculContrat;
            _factureService = factureService;
            _bordereauService = bordereauService;
            _limiteService = limiteService;
        }

        [HttpGet("adherents/contrats")]
        public async Task<IActionResult> GetContratsAdherents()
        {
            try
            {
                // Extract the IndividuId from the JWT token
                var individuIdClaim =  User.FindFirst("id");
                if (individuIdClaim == null)
                {
                    return Unauthorized("IndividuId not found in token.");
                }
            // await    Response.WriteAsJsonAsync(individuIdClaim.Value); 
                int individuId = int.Parse(individuIdClaim.Value);

                var contrats = await _contratService.GetContratsAdherentsByIndividuId(individuId);

                if (contrats.Count == 0)
                {
                    return BadRequest($"L'individu avec l'ID {individuId} n'est pas un adhérent pour tous les contrats.");
                }
                for
                (int i = 0; i < contrats.Count; i++)
                {
                    var invoiceInprogress=await _factureService.GetFactureEnCoursByContratIdAsync(contrats[i].ContratId);
                    var sumBorduro=await _bordereauService.GetBordereauApprouvedSumByContratIdAsync(contrats[i].ContratId);
                    var garantiePercentage=sumBorduro/(100/contrats[i].FondGarantie);
                    var limiteSum=await _limiteService.GetLimitApprouvedSumByContratIdAsync(contrats[i].ContratId);
                    var LimiteDepacement=limiteSum-invoiceInprogress>0?limiteSum-invoiceInprogress:0;
              contrats[i].MontantContrat=invoiceInprogress-garantiePercentage-LimiteDepacement;
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
       /* [HttpGet("contrats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllContrats()
        {
            var contrats = await _contratService.GetAllContratsAsync();
            return Ok(contrats);
        }*/

        [HttpGet("montant/{contratId}")]
        public async Task<IActionResult> GetMontantTotalApprouve(int contratId)
        {
            try
            {
                // Extract the IndividuId from the JWT token
                var individuIdClaim = User.FindFirst("id");
                if (individuIdClaim == null)
                {
                    return Unauthorized("IndividuId not found in token.");
                }
                // await    Response.WriteAsJsonAsync(individuIdClaim.Value); 
                int individuId = int.Parse(individuIdClaim.Value);
                var totalMontantApprouve = await _calculContrat.CalculerMontantTotalApprouveAsync(contratId);
                return Ok(new { ContratId = contratId, MontantTotalApprouve = totalMontantApprouve });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erreur lors du calcul du montant total approuvé", Erreur = ex.Message });
            }
        }
    }
}
