using factoring1.Models;
namespace factoring1.DTO
{
    public class AcheteurFactureSumWithStatus
    {
        public required Individu Acheteur { get; set; }
        public required decimal FactureInProgressSum { get; set; }
        public required decimal FacturePaidSum { get; set; }
    }
}