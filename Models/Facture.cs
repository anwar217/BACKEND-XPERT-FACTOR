using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace factoring1.Models
{
    public class Facture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FactureId { get; set; }
         public FactureStatus Status { get; set; }= FactureStatus.pending;
        [Required]
        public decimal MontantDocument { get; set; }

        [Required]
        [StringLength(100)]
        public string RefFacture { get; set; } = string.Empty;

        [Required]
        public int Echeance { get; set; }
       

        [Required]
        public DateTime DateFacture { get; set; }

        [Required]
        [StringLength(100)]
        public string ModeReglement { get; set; } = string.Empty;

        public int BordereauId { get; set; }
        public Bordereau? Bordereau { get; set; }

        public int IndividuId { get; set; }
        public Individu? Individu { get; set; }

        [Required(ErrorMessage = "Le champ Contrat est requis.")]
        public int ContratId { get; set; }
        public Contrat? Contrat { get; set; }
        public List<Litige>? Litiges { get; set; } = new List<Litige>();
        public List<Prorogation>? Prorogations { get; set; } = new List<Prorogation>();
        public enum FactureStatus{
            paid,
            inProgress,
            pending,
        }
    }
}
