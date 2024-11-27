using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using factoring1.Models;
using factoring1.Services;
using Microsoft.AspNetCore.Authorization;
using factoring1.DTO;

namespace factoring1.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
   
    public class IndividuController : ControllerBase
    {
        private readonly IIndividuService _individuService;
        private readonly IIndividuContratService _individuContratService;
        private readonly IIndividuRepository _individuRepository;

        public IndividuController(IIndividuService individuService, IIndividuContratService individuContratService, IIndividuRepository individuRepository)
        {
            _individuService = individuService;
            _individuContratService = individuContratService;
            _individuRepository = individuRepository;
        }

        [HttpGet("acheteurs/contrat/{contratId}")]
        public async Task<IActionResult> GetAcheteursByContrat(int contratId)
        {
            try
            {
                // Log all claims to debug what claims are available
                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }

                // Extract the "id" claim from the token, which represents the AdherentId
                var adherentIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");

                if (adherentIdClaim == null)
                {
                    return Unauthorized("Adherent non trouvé dans le token.");
                }

                int adherentId = int.Parse(adherentIdClaim.Value);

                var acheteurs = await _individuService.GetAcheteursByContrat(contratId, adherentId);

                if (acheteurs == null || acheteurs.Count == 0)
                {
                    return NotFound("Aucun acheteur trouvé pour ce contrat.");
                }
                return Ok(acheteurs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpPost("{contratId}/ajouter-acheteurs")]
        public async Task<IActionResult> AjouterAcheteursAuContrat(int contratId, [FromBody] List<int> acheteurIds)
        {
            try
            {
                // Récupérer l'ID de l'adhérent à partir du token JWT
                var adherentId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);

                // Appeler le service pour ajouter les acheteurs au contrat
                var result = await _individuContratService.AjouterAcheteursAuContrat(adherentId, contratId, acheteurIds);

                if (result)
                {
                    return Ok("Acheteurs ajoutés avec succès au contrat.");
                }

                return BadRequest("Erreur lors de l'ajout des acheteurs.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("acheteurs")]
        public async Task<IActionResult> UpdateRolesToAcheteur([FromBody] List<int> individuIds)
        {
            try
            {
                await _individuService.UpdateRolesToAcheteurAsync(individuIds);
                return Ok("Roles updated to Acheteur.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpGet("individusRoleIndividu/{contratId}")]
        public async Task<IActionResult> GetIndividusWithRoleIndividu(int contratId)
        {
            try
            {
                var individus = await _individuService.GetIndividusWithRoleIndividuAsync( contratId);
                return Ok(individus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GetAdherentProfile(int id)
        {
            var adherentProfile = await _individuService.GetAdherentProfileAsync(id);
            if (adherentProfile == null)
            {
                return NotFound("Adherent not found");
            }
            return Ok(adherentProfile);
        }


        [HttpPut("{id}/profile")]
        public async Task<IActionResult> UpdateAdherentProfile(int id, [FromBody] Individu updatedProfile)
        {
            if (id != updatedProfile.IndividuId)
            {
                return BadRequest("Individu ID mismatch");
            }

            var existingAdherent = await _individuService.GetAdherentProfileAsync(id);
            if (existingAdherent == null)
            {
                return NotFound("Adherent not found");
            }

            existingAdherent.Nom = updatedProfile.Nom;
            existingAdherent.Prenom = updatedProfile.Prenom;
            existingAdherent.Email = updatedProfile.Email;
            // Mettez à jour d'autres propriétés si nécessaire

            var result = await _individuService.UpdateAdherentProfileAsync(existingAdherent);
            if (!result)
            {
                return StatusCode(500, "An error occurred while updating the profile");
            }

            return NoContent();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateIndividu([FromBody] Individu individu)
        {
            if (individu == null)
            {
                return BadRequest("Individu is null.");
            }

            var result = await _individuService.CreateIndividuAsync(individu);
            if (result != null)
            {
                return Ok(result); // Retourne l'individu créé avec un statut 200 OK
            }
            return BadRequest("Unable to add Individu."); // En cas d'erreur
        }


        [HttpGet("individus")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllIndividus()
        {
            var individus = await _individuService.GetAllIndividus();
            return Ok(individus);
        }

        // Récupérer un individu spécifique avec ses relations
        [HttpGet("individus/{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetIndividuById(int id)
        {
            var individu = await _individuRepository.GetIndividuByIdAsync(id);
            if (individu == null)
            {
                return NotFound();
            }
            return Ok(individu);
        }
        [HttpGet("admin/Adherents")]
        public async  Task<List<AdherentContratMontantCount>> GetAllAdherents(){
            return await _individuService.GetAllAdherents();
        }
        [HttpGet("admin/Acheteurs")]
        public async  Task<List<AcheteurFactureSumWithStatus>> GetAllAcheteurs(){
            return await _individuService.GetAllAcheteurs();
        }
    }
}

