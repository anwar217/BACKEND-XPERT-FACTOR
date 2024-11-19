using factoring1.Models;

namespace factoring1.DTO
{
    public class FactureWithCountDto
    {
        public Facture Facture { get; set; }
        public int LitigeCount { get; set; }
        public int ProrogationCount {  get; set; }
    }
}
