using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace factoring1.Models
{
    public class Prorogation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProrogationId { get; set; }

      

        [Required]
        public DateTime Echeance { get; set; }

        public string MotifProrogation { get; set; }
        [Required]
        public StatusProrogation statut { get; set; }

        [Required]
        public DateTime DateEcheanceApresProrogation { get; set; }
        public int ContratId { get; set; }
        public Contrat? Contrat { get; set; }
        public int FactureId { get; set; }
        public Facture? Facture { get; set; }
        public enum StatusProrogation
        {
            Approuved,
            Rejected,
            Pending
        }
    }
}
