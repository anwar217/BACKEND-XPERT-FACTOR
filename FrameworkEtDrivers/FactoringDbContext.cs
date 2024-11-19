using factoring1.Models;
using Microsoft.EntityFrameworkCore;

namespace factoring1.FrameworkEtDrivers
{
    public class FactoringDbContext : DbContext
    {
        public FactoringDbContext(DbContextOptions<FactoringDbContext> options) : base(options)
        {
        }

        public DbSet<Contrat> Contrats { get; set; }
        public DbSet<Individu> Individus { get; set; }
        public DbSet<IndividuContrat> IndividuContrats { get; set; }
        public DbSet<Bordereau> Bordereaux { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Financement> Financements { get; set; }
        public DbSet<Litige> Litiges { get; set; }
        public DbSet<Prorogation> Prorogations { get; set; }
        public DbSet<Limite> Limites { get; set; }
        public DbSet<Disponible> Disponibles { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the many-to-many relationship between Individu and Contrat through IndividuContrat
            modelBuilder.Entity<IndividuContrat>()
                .HasKey(ic => new { ic.IndividuId, ic.ContratId });

            modelBuilder.Entity<IndividuContrat>()
                .HasOne(ic => ic.Individu)
                .WithMany(i => i.IndividuContrats)
                .HasForeignKey(ic => ic.IndividuId);

            modelBuilder.Entity<IndividuContrat>()
                .HasOne(ic => ic.Contrat)
                .WithMany(c => c.IndividuContrats)
                .HasForeignKey(ic => ic.ContratId);

            modelBuilder.Entity<IndividuContrat>()
                .Property(ic => ic.Role)
                .HasConversion(
                    v => v.ToString(),
                    v => (IndividuContrat.RoleType)Enum.Parse(typeof(IndividuContrat.RoleType), v));

            // Configure Bordereau to Contrat relationship
            modelBuilder.Entity<Bordereau>()
                .HasOne(b => b.Contrat)
                .WithMany(c => c.Bordereaux)
                .HasForeignKey(b => b.ContratId);

            modelBuilder.Entity<Bordereau>()
              .Property(ic => ic.Statut)
              .HasConversion(
                  v => v.ToString(),
                  v => (Bordereau.StatusBordereau)Enum.Parse(typeof(Bordereau.StatusBordereau), v));

            modelBuilder.Entity<Limite>()
               .Property(ic => ic.statut)
               .HasConversion(
                   v => v.ToString(),
                   v => (Limite.StatusLimit)Enum.Parse(typeof(Limite.StatusLimit), v));

            modelBuilder.Entity<Litige>()
              .Property(ic => ic.statut)
              .HasConversion(
                  v => v.ToString(),
                  v => (Litige.StatusLitige)Enum.Parse(typeof(Litige.StatusLitige), v));

            modelBuilder.Entity<Prorogation>()
              .Property(ic => ic.statut)
              .HasConversion(
                  v => v.ToString(),
                  v => (Prorogation.StatusProrogation)Enum.Parse(typeof(Prorogation.StatusProrogation), v));

            // Configure Facture relationships
            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Bordereau)
                .WithMany(b => b.Factures)
                .HasForeignKey(f => f.BordereauId);

            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Contrat)
                .WithMany(c => c.Factures)
                .HasForeignKey(f => f.ContratId);

            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Individu)
                .WithMany(i => i.Factures)
                .HasForeignKey(f => f.IndividuId);
            modelBuilder.Entity<Facture>()
                .HasMany(f => f.Litiges)
                .WithOne(l => l.Facture)
                .HasForeignKey(l => l.FactureId);

            modelBuilder.Entity<Facture>()
                .HasMany(f => f.Prorogations)
                .WithOne(p => p.Facture)
                .HasForeignKey(p => p.FactureId);

            modelBuilder.Entity<Financement>()
                .HasOne(f => f.Contrat)
                .WithMany(c => c.Financements)
                .HasForeignKey(f => f.ContratId);

            modelBuilder.Entity<Financement>()
             .Property(f => f.TypeDeFinancement)
             .HasConversion(
                 v => v.ToString(),
                 v => (TypeDeFinancement)Enum.Parse(typeof(TypeDeFinancement), v));

       
            modelBuilder.Entity<Financement>()
      .Property(f => f.StatutFinancement)
      .HasConversion(
          v => v.ToString(),
          v => (StatutFinancement)Enum.Parse(typeof(StatutFinancement), v)
      );




            modelBuilder.Entity<Litige>()
               .HasOne(l => l.Contrat)
               .WithMany(c => c.Litiges)
               .HasForeignKey(l => l.ContratId);

            modelBuilder.Entity<Prorogation>()
               .HasOne(p => p.Contrat)
               .WithMany(c => c.Prorogations)
               .HasForeignKey(p => p.ContratId);

            modelBuilder.Entity<Limite>()
               .HasOne(l => l.Contrat)
               .WithMany(c => c.Limites)
               .HasForeignKey(l => l.ContratId);

modelBuilder.Entity<Disponible>()
   .HasOne(D => D.Contrat)
   .WithMany(c => c.Disponibles)
   .HasForeignKey(D => D.ContratId);
            // Configure Disponible as owned entity types (if needed)


            base.OnModelCreating(modelBuilder);
        }
    }
}
