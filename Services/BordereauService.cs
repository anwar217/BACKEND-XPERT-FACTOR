using factoring1.Models;
using factoring1.Repositories;
using System.Threading.Tasks;

namespace factoring1.Services
{
    public class BordereauService : IBordereauService
    {
        private readonly IBordereauRepository _bordereauRepository;

        public BordereauService(IBordereauRepository bordereauRepository)
        {
            _bordereauRepository = bordereauRepository;
        }

        public async Task<Bordereau> AddBordereauAsync(Bordereau bordereau)
        {
            // Validate the total amount
            decimal totalAmount = 0;
            foreach (var facture in bordereau.Factures)
            {
                totalAmount += facture.MontantDocument;
            }

            if (totalAmount != bordereau.MontantTotal)
            {
                throw new ArgumentException("Le montant total du bordereau doit être égal à la somme des montants des factures.");
            }

            return await _bordereauRepository.AddBordereauAsync(bordereau);
        }
        public async Task<List<Bordereau>> GetBordereauxByContratAndIndividuAsync(int contratId, int individuId)
        {
            return await _bordereauRepository.GetBordereauxByContratAndIndividuAsync(contratId, individuId);
        }
    }
}
