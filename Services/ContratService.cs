using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using factoring1.Repositories;
using Microsoft.EntityFrameworkCore;

namespace factoring1.Services
{
    public class ContratService : IContratService
    {
        private readonly IContratRepository _contratRepository;
        private readonly IIndividuRepository _individuRepository;
        private readonly FactoringDbContext _context;


        public ContratService(IContratRepository contratRepository,IIndividuRepository individuRepository, FactoringDbContext context)
        {
            _contratRepository = contratRepository;
            _individuRepository = individuRepository;
            _context = context;
        }

       

        public async Task<List<Contrat>> GetContratsAdherentsByIndividuId(int individuId)
        {
            var contrats = await _contratRepository.GetContratsByIndividuId(individuId);

            // Vérifier si tous les contrats sont de type Adherent
            bool tousAdherents = contrats.All(contrat =>
                contrat.IndividuContrats.Any(ic => ic.IndividuId == individuId && ic.Role == IndividuContrat.RoleType.Adherent));

            if (!tousAdherents)
            {
                return new List<Contrat>(); // Retourner une liste vide si pas tous les contrats sont adhérents
            }

            return contrats;
        }

        public async Task<List<Contrat>> GetContratsByIndividuId(int individuId)
        {
            return await _contratRepository.GetContratsByIndividuId(individuId);
        }

        public async Task<Contrat> GetContratByIndividuIdAndContratId(int individuId, int contratId)
        {
            var contrat = await _contratRepository.GetContratByIdAndIndividuId(contratId, individuId);

            if (contrat == null)
            {
                throw new ArgumentException($"Contrat avec ID {contratId} n'a pas été trouvé pour l'individu avec ID {individuId}.");
            }

            bool isAdherent = contrat.IndividuContrats.Any(ic => ic.IndividuId == individuId && ic.Role == IndividuContrat.RoleType.Adherent);
            if (!isAdherent)
            {
                throw new ArgumentException($"L'individu avec l'ID {individuId} n'est pas un adhérent pour le contrat avec ID {contratId}.");
            }

            return contrat;
        }

        public async Task<Contrat> AddContratToAdherentAsync(int individuId, Contrat contrat)
        {
            // Vérifier si l'individu existe
            var individu = await _individuRepository.GetIndividuByIdAsync(individuId); // Vous devez implémenter cette méthode dans votre repository
            if (individu == null)
            {
                throw new ArgumentException("Individu non trouvé.");
            }

            // Créer une association entre l'adhérent et le contrat
            var individuContrat = new IndividuContrat
            {
                IndividuId = individuId,
                ContratId = contrat.ContratId,
                Role = IndividuContrat.RoleType.Adherent // Associer l'individu au rôle d'adhérent
            };

            // Ajouter le contrat à la base de données
            _context.IndividuContrats.Add(individuContrat);
            await _context.SaveChangesAsync();

            return contrat; // Retourner le contrat créé
        }
        public async Task<List<Contrat>> GetAllContratsAsync()
        {
            return await _contratRepository.GetAllContratsAsync();
        }
        public async Task<Contrat> GetContratByIdAsync(int contratId)
        {
            return await _contratRepository.GetContratByIdAsync(contratId);
        }

    }


}
