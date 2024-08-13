using System.ComponentModel.DataAnnotations;

namespace factoring1.Models
{
    public class IndividuContrat
    {
        public int IndividuId { get; set; }
        public Individu? Individu { get; set; }

        public int ContratId { get; set; }
        public Contrat? Contrat { get; set; }

        [Required]
        public RoleType Role { get; set; }

        public enum RoleType
        {
            Adherent,
            Acheteur,
            Individu
        }
    }
}
