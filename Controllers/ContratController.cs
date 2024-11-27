using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using factoring1.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using factoring1.Models;
using factoring1.Repositories;
using factoring1.DTO;

namespace factoring1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 //[Authorize] 
    public class ContratController(IContratService contratService, CalculContrat calculContrat, IFactureService factureService, IBordereauService bordereauService, ILimiteService limiteService, IContratRepository contratRepository,IIndividuContratService individuContratService) : ControllerBase
    {
        private readonly IContratService _contratService = contratService;
        private readonly CalculContrat _calculContrat = calculContrat;
        private readonly IFactureService _factureService = factureService;
        private readonly IBordereauService _bordereauService = bordereauService;
        private readonly ILimiteService _limiteService = limiteService;
        private readonly IContratRepository  _contratRepository = contratRepository;
        private readonly IIndividuContratService _individuContratService = individuContratService;

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
        [HttpGet("contrats")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllContrats()
        {
            var contrats = await _contratService.GetAllContratsAsync();
            return Ok(contrats);
        }

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
        [HttpPost("contrat/new")]
        public async Task<IActionResult> Create([FromBody] Contrat contrat){
            Console.WriteLine(contrat);
            try
            {
                  var newContrat = await _contratRepository.CreateContratAsync(contrat);
                return Ok(newContrat);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            
                return BadRequest("Error creating contrat");
            }
          
        }
        [HttpGet("admin/{contratId}")]
        public async Task<IActionResult> GetContratAdminById(int contratId){
            try {
                var contrat = await _contratService.GetContratAdminByIdAsync(contratId);
         
            return Ok(contrat);
            }
          
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }
        [HttpPost("admin/linkContratToAdherent")]
        public async Task<IActionResult> LinkContratToAdherent([FromBody] ContractLinkCredencials data){
            try {
                var contrat = await _individuContratService.LinkContractToAdherent(data.ContratId,data.AdherentId);
                return Ok(contrat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }
    }

}
