﻿using System.Collections.Generic;
using System.Threading.Tasks;
using factoring1.Models;

public interface IIndividuRepository
{
    Task<List<Individu>> GetAcheteursByContrat(int contratId,int adherantId);
    Task UpdateRolesToAcheteurAsync(IEnumerable<int> individuIds);
    Task<IEnumerable<Individu>> GetIndividusWithRoleIndividuAsync(int contratId);
    Task<Individu?> GetAdherentProfileAsync(int individuId);
    Task<bool> UpdateAdherentProfileAsync(Individu individu);




}
