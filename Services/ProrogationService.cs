﻿using System.Threading.Tasks;
using factoring1.DTO;
using factoring1.FrameworkEtDrivers;
using factoring1.Models;
using factoring1.Repositories;
using Microsoft.EntityFrameworkCore;

namespace factoring1.Services
{


    public class ProrogationService : IProrogationService
    {
        private readonly FactoringDbContext _context;


        private readonly IProrogationRepository _prorogationRepository;

            public ProrogationService(IProrogationRepository prorogationRepository,FactoringDbContext context)
            {
                _prorogationRepository = prorogationRepository;
            _context = context;
            }

            public async Task<Prorogation> AddProrogationAsync(int contratId, int factureId, Prorogation prorogation)
            {
              

                

                prorogation.ContratId = contratId;
                prorogation.FactureId = factureId;
                return await _prorogationRepository.AddProrogationAsync(prorogation);
            }
        public async Task<IEnumerable<Prorogation>> GetProrogationByFacture(int factureId)
        {
            var litiges = await _context.Prorogations
                .Where(l => l.FactureId == factureId)
                .ToListAsync();

            return litiges;
        }
        public async Task<Prorogation> ValidateProrogationAsync(ProrogationValidatiteCredencials credencials){

            var prorogation = await _context.Prorogations.FirstOrDefaultAsync(l => l.ProrogationId == credencials.ProrogationId );

            if (prorogation != null)
            {
                if(credencials.Action=="refuse")
                {
                    prorogation.Statut = Prorogation.StatusProrogation.Rejected;
                }
                if(credencials.Action=="accept")
                {
                    prorogation.Statut = Prorogation.StatusProrogation.Approuved;
                }
                _context.Prorogations.Update(prorogation);
                await _context.SaveChangesAsync();
            }
            return prorogation;
        }
    }
    
}
