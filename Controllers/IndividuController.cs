using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using factoring1.Models;
using factoring1.Services;
using Microsoft.AspNetCore.Authorization;

namespace factoring1.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
   
    public class IndividuController : ControllerBase
    {
        private readonly IIndividuService _individuService;

        public IndividuController(IIndividuService individuService)
        {
            _individuService = individuService;
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

        [HttpGet("individusRoleIndividu")]
        public async Task<IActionResult> GetIndividusWithRoleIndividu()
        {
            try
            {
                var individus = await _individuService.GetIndividusWithRoleIndividuAsync();
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
    }
}

