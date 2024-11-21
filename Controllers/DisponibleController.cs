using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.Models;
using factoring1.Services;
using System.Diagnostics;
namespace factoring1.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DisponibleController(IDisponibleService disponibleService, IFactureService factureService,IBordereauService bordereauService ,IContratService contratService,ILimiteService limiteService) : ControllerBase
{
    private readonly IDisponibleService _disponibleService = disponibleService;
    private readonly IFactureService _factureService = factureService;
    private readonly IBordereauService _bordereauService = bordereauService;
    private readonly IContratService _contratService = contratService;
    private readonly ILimiteService _limiteService = limiteService;

    [HttpGet("{contratId}")]
    public async Task<ActionResult<Disponible>> GetDisponiblesByContratId(int contratId)
    {
    var disponible= new ContractStats();
    var sumBorduro=await _bordereauService.GetBordereauApprouvedSumByContratIdAsync(contratId);

    var contract=await _contratService.GetContratByIdAsync(contratId);
    var contractFound=contract.FondGarantie;
    var garantiePercentage=sumBorduro/(100/contractFound);
    var factureInProgressSum=await  _factureService.GetFactureEnCoursByContratIdAsync(contratId);
    var approuvedfactureSum=await _factureService.GetFacturesApprouvedByContratIdAsync(contratId);
    disponible.FuctureApprouved=approuvedfactureSum;
    disponible.FondsDeGaranties=garantiePercentage;
    disponible.ContratId = contratId;
    disponible.FactureEnCours=factureInProgressSum;
    disponible.ContractFound=sumBorduro;
    var limiteSum=await _limiteService.GetLimitApprouvedSumByContratIdAsync(contratId);
    disponible.DepassementLimiteFinancementAcheteurs=limiteSum-factureInProgressSum>0?limiteSum-factureInProgressSum:0;
    disponible.LimitSum=limiteSum;

        return Ok(disponible);
    }

    [HttpPost]
    public async Task<ActionResult<Disponible>> AddDisponible(Disponible disponible)
    {
        var addedDisponible = await _disponibleService.AddDisponible(disponible);
        return CreatedAtAction(nameof(GetDisponiblesByContratId), new { contratId = addedDisponible.ContratId }, addedDisponible);
    }
}
class ContractStats
{
    public int ContratId { get; set; }
    public decimal FondsDeGaranties { get; set; }
    public decimal FactureEnCours { get; set; }
    public decimal DepassementLimiteFinancementAcheteurs { get; set; }
    public decimal FuctureApprouved { get; set; }
    public decimal ContractFound { get; set; }
    public decimal LimitSum { get; set; }
}