using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace factoring1.Models
{
    public class Disponible
    {
        [Key]
        public int DisponibleId { get; set; }

        [Required]
        public FormuleType Formule { get; set; }

        [Required]
        public int FactureEnCours { get; set; }

        [Required]
        public int FondsDeGaranties { get; set; }

        [Required]
        public int FondsDeReserve { get; set; }

        [Required]
        public int DepassementLimiteFinancementAcheteurs { get; set; }
         public int ContratId { get; set; }
        public Contrat? Contrat { get; set; }
    }
    public enum FormuleType
    {
     
     [Description("Formule 1 for disponible contrat")]  
      Formule1=1,

    [Description("Formule 2 for disponible contrat")]
        Formule2=2
    }
}