﻿using factoring1.Models;

namespace factoring1.Services
{
    public interface IFactureService
    {
        Task<List<Facture>> GetFacturesByContratIdAsync(int contratId);

    }
}
