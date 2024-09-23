using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace factoring1.Models
{
    public class Contrat
    {
        public int ContratId { get; set; }

        [Required]
        public string ReferenceContrat { get; set; } = string.Empty;

        [Required]
        public decimal MontantContrat { get; set; }

        [Required]
        public string StatutContrat { get; set; }

        [Required]
        public string TypeContrat { get; set; }

        [Required]
        public string DeviceContrat { get; set; }

        [Required]
        public DateTime DateDebContrat { get; set; }

        [Required]
        public DateTime DateSignContrat { get; set; }

        public List<IndividuContrat>? IndividuContrats { get; set; } = new List<IndividuContrat>();
        public List<Bordereau>? Bordereaux { get; set; } = new List<Bordereau>();
        public List<Facture>? Factures { get; set; } = new List<Facture>();
        public List<Financement>? Financements { get; set; } = new List<Financement>();
        public List<Litige>? Litiges { get; set; } = new List<Litige>();
        public List<Prorogation>? Prorogations { get; set; } = new List<Prorogation>();
        public List<Limite>? Limites { get; set; } = new List<Limite>();

        // Nouvelle propriété pour les disponibles
        public List<Disponible>? Disponibles { get; set; } = new List<Disponible>();
    }
}