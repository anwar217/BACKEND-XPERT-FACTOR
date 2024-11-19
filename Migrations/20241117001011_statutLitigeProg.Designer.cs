﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using factoring1.FrameworkEtDrivers;

#nullable disable

namespace factoring1.Migrations
{
    [DbContext(typeof(FactoringDbContext))]
    [Migration("20241117001011_statutLitigeProg")]
    partial class statutLitigeProg
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("factoring1.Models.Bordereau", b =>
                {
                    b.Property<int>("BordereauId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("BordereauId"));

                    b.Property<int>("AnneeBordereau")
                        .HasColumnType("int");

                    b.Property<int>("ContratId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateBordereau")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("MontantTotal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("NombreDocuments")
                        .HasColumnType("int");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("BordereauId");

                    b.HasIndex("ContratId");

                    b.ToTable("Bordereaux");
                });

            modelBuilder.Entity("factoring1.Models.Contrat", b =>
                {
                    b.Property<int>("ContratId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ContratId"));

                    b.Property<DateTime>("DateDebContrat")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateSignContrat")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeviceContrat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FondGarantie")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FraisCreation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FraisLimite")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("MontantContrat")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ReferenceContrat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StatutContrat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TypeContrat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ContratId");

                    b.ToTable("Contrats");
                });

            modelBuilder.Entity("factoring1.Models.Disponible", b =>
                {
                    b.Property<int>("DisponibleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DisponibleId"));

                    b.Property<int>("ContratId")
                        .HasColumnType("int");

                    b.Property<int>("DepassementLimiteFinancementAcheteurs")
                        .HasColumnType("int");

                    b.Property<int>("FactureEnCours")
                        .HasColumnType("int");

                    b.Property<int>("FondsDeGaranties")
                        .HasColumnType("int");

                    b.Property<int>("FondsDeReserve")
                        .HasColumnType("int");

                    b.Property<int>("Formule")
                        .HasColumnType("int");

                    b.HasKey("DisponibleId");

                    b.HasIndex("ContratId");

                    b.ToTable("Disponibles");
                });

            modelBuilder.Entity("factoring1.Models.Facture", b =>
                {
                    b.Property<int>("FactureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("FactureId"));

                    b.Property<int>("BordereauId")
                        .HasColumnType("int");

                    b.Property<int>("ContratId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFacture")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Echeance")
                        .HasColumnType("int");

                    b.Property<int>("IndividuId")
                        .HasColumnType("int");

                    b.Property<string>("ModeReglement")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("MontantDocument")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("RefFacture")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("FactureId");

                    b.HasIndex("BordereauId");

                    b.HasIndex("ContratId");

                    b.HasIndex("IndividuId");

                    b.ToTable("Factures");
                });

            modelBuilder.Entity("factoring1.Models.Financement", b =>
                {
                    b.Property<int>("FinancementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("FinancementId"));

                    b.Property<int>("ContratId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDeFinancement")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MethodeDePaiement")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("MontantFinancement")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("StatutFinancement")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TypeDeFinancement")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("FinancementId");

                    b.HasIndex("ContratId");

                    b.ToTable("Financements");
                });

            modelBuilder.Entity("factoring1.Models.Individu", b =>
                {
                    b.Property<int>("IndividuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IndividuId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumberPhone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IndividuId");

                    b.ToTable("Individus");
                });

            modelBuilder.Entity("factoring1.Models.IndividuContrat", b =>
                {
                    b.Property<int>("IndividuId")
                        .HasColumnType("int");

                    b.Property<int>("ContratId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IndividuId", "ContratId");

                    b.HasIndex("ContratId");

                    b.ToTable("IndividuContrats");
                });

            modelBuilder.Entity("factoring1.Models.Limite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContratId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateDemande")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateDerniereDemande")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateLimite")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DelaiDemande")
                        .HasColumnType("int");

                    b.Property<decimal>("LimiteAssurance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("LimiteFinancement")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ModePaiement")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("statut")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ContratId");

                    b.ToTable("Limites");
                });

            modelBuilder.Entity("factoring1.Models.Litige", b =>
                {
                    b.Property<int>("LitigeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("LitigeId"));

                    b.Property<int>("ContratId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEcheanceLitige")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateLitige")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FactureId")
                        .HasColumnType("int");

                    b.Property<string>("TypeDuLitige")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("statut")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("LitigeId");

                    b.HasIndex("ContratId");

                    b.HasIndex("FactureId");

                    b.ToTable("Litiges");
                });

            modelBuilder.Entity("factoring1.Models.Prorogation", b =>
                {
                    b.Property<int>("ProrogationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProrogationId"));

                    b.Property<int>("ContratId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEcheanceApresProrogation")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Echeance")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FactureId")
                        .HasColumnType("int");

                    b.Property<string>("MotifProrogation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TypeProrogation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("statut")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ProrogationId");

                    b.HasIndex("ContratId");

                    b.HasIndex("FactureId");

                    b.ToTable("Prorogations");
                });

            modelBuilder.Entity("factoring1.Models.Bordereau", b =>
                {
                    b.HasOne("factoring1.Models.Contrat", "Contrat")
                        .WithMany("Bordereaux")
                        .HasForeignKey("ContratId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contrat");
                });

            modelBuilder.Entity("factoring1.Models.Disponible", b =>
                {
                    b.HasOne("factoring1.Models.Contrat", "Contrat")
                        .WithMany("Disponibles")
                        .HasForeignKey("ContratId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contrat");
                });

            modelBuilder.Entity("factoring1.Models.Facture", b =>
                {
                    b.HasOne("factoring1.Models.Bordereau", "Bordereau")
                        .WithMany("Factures")
                        .HasForeignKey("BordereauId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("factoring1.Models.Contrat", "Contrat")
                        .WithMany("Factures")
                        .HasForeignKey("ContratId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("factoring1.Models.Individu", "Individu")
                        .WithMany("Factures")
                        .HasForeignKey("IndividuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bordereau");

                    b.Navigation("Contrat");

                    b.Navigation("Individu");
                });

            modelBuilder.Entity("factoring1.Models.Financement", b =>
                {
                    b.HasOne("factoring1.Models.Contrat", "Contrat")
                        .WithMany("Financements")
                        .HasForeignKey("ContratId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contrat");
                });

            modelBuilder.Entity("factoring1.Models.IndividuContrat", b =>
                {
                    b.HasOne("factoring1.Models.Contrat", "Contrat")
                        .WithMany("IndividuContrats")
                        .HasForeignKey("ContratId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("factoring1.Models.Individu", "Individu")
                        .WithMany("IndividuContrats")
                        .HasForeignKey("IndividuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contrat");

                    b.Navigation("Individu");
                });

            modelBuilder.Entity("factoring1.Models.Limite", b =>
                {
                    b.HasOne("factoring1.Models.Contrat", "Contrat")
                        .WithMany("Limites")
                        .HasForeignKey("ContratId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contrat");
                });

            modelBuilder.Entity("factoring1.Models.Litige", b =>
                {
                    b.HasOne("factoring1.Models.Contrat", "Contrat")
                        .WithMany("Litiges")
                        .HasForeignKey("ContratId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("factoring1.Models.Facture", "Facture")
                        .WithMany("Litiges")
                        .HasForeignKey("FactureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contrat");

                    b.Navigation("Facture");
                });

            modelBuilder.Entity("factoring1.Models.Prorogation", b =>
                {
                    b.HasOne("factoring1.Models.Contrat", "Contrat")
                        .WithMany("Prorogations")
                        .HasForeignKey("ContratId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("factoring1.Models.Facture", "Facture")
                        .WithMany("Prorogations")
                        .HasForeignKey("FactureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contrat");

                    b.Navigation("Facture");
                });

            modelBuilder.Entity("factoring1.Models.Bordereau", b =>
                {
                    b.Navigation("Factures");
                });

            modelBuilder.Entity("factoring1.Models.Contrat", b =>
                {
                    b.Navigation("Bordereaux");

                    b.Navigation("Disponibles");

                    b.Navigation("Factures");

                    b.Navigation("Financements");

                    b.Navigation("IndividuContrats");

                    b.Navigation("Limites");

                    b.Navigation("Litiges");

                    b.Navigation("Prorogations");
                });

            modelBuilder.Entity("factoring1.Models.Facture", b =>
                {
                    b.Navigation("Litiges");

                    b.Navigation("Prorogations");
                });

            modelBuilder.Entity("factoring1.Models.Individu", b =>
                {
                    b.Navigation("Factures");

                    b.Navigation("IndividuContrats");
                });
#pragma warning restore 612, 618
        }
    }
}
