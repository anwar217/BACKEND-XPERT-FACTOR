using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace factoring1.Models
{
    public class Individu
    {
        public int IndividuId { get; set; }

        [Required]
        public string Nom { get; set; } 

        [Required]
        public string Prenom { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public string NumberPhone { get; set; }
        public bool IsAdmin { get; set; } = false;




        public List<IndividuContrat>? IndividuContrats { get; set; } = new List<IndividuContrat>();
        public List<Facture>? Factures { get; set; } = new List<Facture>();
    }
}
