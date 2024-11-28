using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace factoring1.Models
{
    public class Litige
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LitigeId { get; set; }

        [Required]
        [StringLength(100)]
        public required string TypeDuLitige { get; set; }

        [Required]
        public DateTime DateLitige { get; set; }
        [Required]
        public StatusLitige Statut { get; set; }=StatusLitige.Pending;

        [Required]
        public DateTime DateEcheanceLitige { get; set; }
        public int ContratId { get; set; }
        public Contrat? Contrat { get; set; }
        public int FactureId { get; set; }
        public Facture? Facture { get; set; }
        public enum StatusLitige
        {
            Approuved,
            Rejected,
            Pending
        }
    }

    }
