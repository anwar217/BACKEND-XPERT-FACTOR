using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace factoring1.Models
{
    public class Limite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime? DateDemande { get; set; }

        [Required]
        public decimal LimiteAssurance { get; set; }

        [Required]
        public decimal LimiteFinancement { get; set; }
        [Required]
        public StatusLimit Status { get; set; }= StatusLimit.Pending;

        [Required]
        public DateTime? DateLimite { get; set; }

        public DateTime? DateDerniereDemande { get; set; }

        [Required]
        public int DelaiDemande { get; set; }


        [StringLength(100)]
        public  string? ModePaiement { get; set; }
        public int ContratId { get; set; }
        public Contrat? Contrat { get; set; }
        public enum StatusLimit
        {
            Approuved,
            Rejected,
            Pending
        }

    }
}
