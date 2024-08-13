using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace factoring1.Models
{
    public class Bordereau
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BordereauId { get; set; }

        [Required]
        public decimal MontantTotal { get; set; }

        [Required]
        public DateTime DateBordereau { get; set; }

        [Required]
        public int NombreDocuments { get; set; }

        [Required]
        public int AnneeBordereau { get; set; }

        public int ContratId { get; set; }
        public Contrat? Contrat { get; set; }

        public List<Facture>? Factures { get; set; } = new List<Facture>();

    }
}
