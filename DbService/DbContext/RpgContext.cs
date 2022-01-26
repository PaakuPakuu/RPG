﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DbService
{
    public partial class RpgContext : DbContext
    {
        public RpgContext()
        {
        }

        public RpgContext(DbContextOptions<RpgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Boutique> Boutique { get; set; }
        public virtual DbSet<CompetenceChoix> CompetenceChoix { get; set; }
        public virtual DbSet<CompetenceHeritee> CompetenceHeritee { get; set; }
        public virtual DbSet<Competences> Competences { get; set; }
        public virtual DbSet<GroupeInventaire> GroupeInventaire { get; set; }
        public virtual DbSet<GroupeMonstre> GroupeMonstre { get; set; }
        public virtual DbSet<Inventaire> Inventaire { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Joueur> Joueur { get; set; }
        public virtual DbSet<Map> Map { get; set; }
        public virtual DbSet<Metier> Metier { get; set; }
        public virtual DbSet<Monstre> Monstre { get; set; }
        public virtual DbSet<Origine> Origine { get; set; }
        public virtual DbSet<RaceMonstre> RaceMonstre { get; set; }
        public virtual DbSet<TypeItem> TypeItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Boutique>(entity =>
            {
                entity.HasKey(e => e.IdBoutique)
                    .HasName("PRIMARY");

                entity.ToTable("boutique");

                entity.Property(e => e.IdBoutique).HasColumnName("id_boutique");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<CompetenceChoix>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("competence_choix");

                entity.HasIndex(e => e.IdOrigine, "competece_choix_origine_idx");

                entity.HasIndex(e => e.IdCompetence, "competence_choix_competence_idx");

                entity.HasIndex(e => e.IdMetier, "competence_choix_metier_idx");

                entity.Property(e => e.IdCompetence).HasColumnName("id_competence");

                entity.Property(e => e.IdMetier).HasColumnName("id_metier");

                entity.Property(e => e.IdOrigine).HasColumnName("id_origine");

                entity.HasOne(d => d.IdCompetenceNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("competence_choix_competence");

                entity.HasOne(d => d.IdMetierNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMetier)
                    .HasConstraintName("competence_choix_metier");

                entity.HasOne(d => d.IdOrigineNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdOrigine)
                    .HasConstraintName("competece_choix_origine");
            });

            modelBuilder.Entity<CompetenceHeritee>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("competence_heritee");

                entity.HasIndex(e => e.IdCompetence, "association_competence_competence_idx");

                entity.HasIndex(e => e.IdOrigine, "association_competence_origine_idx");

                entity.HasIndex(e => e.IdMetier, "competence_heritee_metier_idx");

                entity.Property(e => e.IdCompetence).HasColumnName("id_competence");

                entity.Property(e => e.IdMetier).HasColumnName("id_metier");

                entity.Property(e => e.IdOrigine).HasColumnName("id_origine");

                entity.HasOne(d => d.IdCompetenceNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("competence_heritee_competence");

                entity.HasOne(d => d.IdMetierNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMetier)
                    .HasConstraintName("competence_heritee_metier");

                entity.HasOne(d => d.IdOrigineNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdOrigine)
                    .HasConstraintName("competence_heritee_origine");
            });

            modelBuilder.Entity<Competences>(entity =>
            {
                entity.HasKey(e => e.IdCompetence)
                    .HasName("PRIMARY");

                entity.ToTable("competences");

                entity.Property(e => e.IdCompetence).HasColumnName("id_competence");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<GroupeInventaire>(entity =>
            {
                entity.HasKey(e => e.IdGroupeInventaire)
                    .HasName("PRIMARY");

                entity.ToTable("groupe_inventaire");

                entity.Property(e => e.IdGroupeInventaire).HasColumnName("id_groupe_inventaire");
            });

            modelBuilder.Entity<GroupeMonstre>(entity =>
            {
                entity.HasKey(e => e.IdGroupeMonstre)
                    .HasName("PRIMARY");

                entity.ToTable("groupe_monstre");

                entity.HasIndex(e => e.IdMap, "groupe_monstre_map_idx");

                entity.Property(e => e.IdGroupeMonstre).HasColumnName("id_groupe_monstre");

                entity.Property(e => e.EstBattu).HasColumnName("est_battu");

                entity.Property(e => e.IdMap).HasColumnName("id_map");

                entity.HasOne(d => d.IdMapNavigation)
                    .WithMany(p => p.GroupeMonstre)
                    .HasForeignKey(d => d.IdMap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("groupe_monstre_map");
            });

            modelBuilder.Entity<Inventaire>(entity =>
            {
                entity.HasKey(e => e.IdInventaire)
                    .HasName("PRIMARY");

                entity.ToTable("inventaire");

                entity.HasIndex(e => e.IdBoutique, "boutique_inventaire_idx");

                entity.Property(e => e.IdInventaire).HasColumnName("id_inventaire");

                entity.Property(e => e.IdBoutique).HasColumnName("id_boutique");

                entity.Property(e => e.Slots).HasColumnName("slots");

                entity.Property(e => e.Titre)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("titre");

                entity.HasOne(d => d.IdBoutiqueNavigation)
                    .WithMany(p => p.Inventaire)
                    .HasForeignKey(d => d.IdBoutique)
                    .HasConstraintName("boutique_inventaire");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.IdItem)
                    .HasName("PRIMARY");

                entity.ToTable("item");

                entity.HasIndex(e => e.IdInventaire, "item_inventaire_idx");

                entity.HasIndex(e => e.IdTypeItem, "item_type_item_idx");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.IdInventaire).HasColumnName("id_inventaire");

                entity.Property(e => e.IdTypeItem).HasColumnName("id_type_item");

                entity.Property(e => e.Quantite).HasColumnName("quantite");

                entity.HasOne(d => d.IdInventaireNavigation)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.IdInventaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_inventaire");

                entity.HasOne(d => d.IdTypeItemNavigation)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.IdTypeItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_type_item");
            });

            modelBuilder.Entity<Joueur>(entity =>
            {
                entity.HasKey(e => e.IdJoueur)
                    .HasName("PRIMARY");

                entity.ToTable("joueur");

                entity.HasIndex(e => e.IdInventaire, "joueur_inventaire_idx");

                entity.HasIndex(e => e.IdMapCourante, "joueur_map_idx");

                entity.HasIndex(e => e.IdMetier, "joueur_metier_idx");

                entity.HasIndex(e => e.IdOrigine, "joueur_race_idx");

                entity.Property(e => e.IdJoueur).HasColumnName("id_joueur");

                entity.Property(e => e.Adresse).HasColumnName("adresse");

                entity.Property(e => e.Argent).HasColumnName("argent");

                entity.Property(e => e.Attaque).HasColumnName("attaque");

                entity.Property(e => e.Charisme).HasColumnName("charisme");

                entity.Property(e => e.Courage).HasColumnName("courage");

                entity.Property(e => e.Destin).HasColumnName("destin");

                entity.Property(e => e.EnergieAstrale).HasColumnName("energie_astrale");

                entity.Property(e => e.Experience).HasColumnName("experience");

                entity.Property(e => e.Force).HasColumnName("force");

                entity.Property(e => e.IdInventaire).HasColumnName("id_inventaire");

                entity.Property(e => e.IdMapCourante).HasColumnName("id_map_courante");

                entity.Property(e => e.IdMetier).HasColumnName("id_metier");

                entity.Property(e => e.IdOrigine).HasColumnName("id_origine");

                entity.Property(e => e.Intelligence).HasColumnName("intelligence");

                entity.Property(e => e.Niveau).HasColumnName("niveau");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("nom");

                entity.Property(e => e.Or).HasColumnName("or");

                entity.Property(e => e.Parade).HasColumnName("parade");

                entity.Property(e => e.PointsVie).HasColumnName("points_vie");

                entity.HasOne(d => d.IdInventaireNavigation)
                    .WithMany(p => p.Joueur)
                    .HasForeignKey(d => d.IdInventaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("joueur_inventaire");

                entity.HasOne(d => d.IdMapCouranteNavigation)
                    .WithMany(p => p.Joueur)
                    .HasForeignKey(d => d.IdMapCourante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("joueur_map");

                entity.HasOne(d => d.IdMetierNavigation)
                    .WithMany(p => p.Joueur)
                    .HasForeignKey(d => d.IdMetier)
                    .HasConstraintName("joueur_metier");

                entity.HasOne(d => d.IdOrigineNavigation)
                    .WithMany(p => p.Joueur)
                    .HasForeignKey(d => d.IdOrigine)
                    .HasConstraintName("joueur_origine");
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.HasKey(e => e.IdMap)
                    .HasName("PRIMARY");

                entity.ToTable("map");

                entity.Property(e => e.IdMap).HasColumnName("id_map");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("filename");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("nom");

                entity.Property(e => e.PositionSpawnX).HasColumnName("position_spawn_x");

                entity.Property(e => e.PositionSpawnY).HasColumnName("position_spawn_y");
            });

            modelBuilder.Entity<Metier>(entity =>
            {
                entity.HasKey(e => e.IdMetier)
                    .HasName("PRIMARY");

                entity.ToTable("metier");

                entity.Property(e => e.IdMetier).HasColumnName("id_metier");

                entity.Property(e => e.AdresseMax).HasColumnName("adresse_max");

                entity.Property(e => e.AdresseMin).HasColumnName("adresse_min");

                entity.Property(e => e.Bouclier).HasColumnName("bouclier");

                entity.Property(e => e.Charge).HasColumnName("charge");

                entity.Property(e => e.CharismeMax).HasColumnName("charisme_max");

                entity.Property(e => e.CharismeMin).HasColumnName("charisme_min");

                entity.Property(e => e.CourageMax).HasColumnName("courage_max");

                entity.Property(e => e.CourageMin).HasColumnName("courage_min");

                entity.Property(e => e.DeuxMains).HasColumnName("deux_mains");

                entity.Property(e => e.ForceMax).HasColumnName("force_max");

                entity.Property(e => e.ForceMin).HasColumnName("force_min");

                entity.Property(e => e.IntelligenceMax).HasColumnName("intelligence_max");

                entity.Property(e => e.IntelligenceMin).HasColumnName("intelligence_min");

                entity.Property(e => e.ModificateurPv).HasColumnName("modificateur_pv");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("nom");

                entity.Property(e => e.PointsProtection).HasColumnName("points_protection");
            });

            modelBuilder.Entity<Monstre>(entity =>
            {
                entity.HasKey(e => e.IdMonstre)
                    .HasName("PRIMARY");

                entity.ToTable("monstre");

                entity.HasIndex(e => e.IdGroupe, "monstre_groupe_idx");

                entity.HasIndex(e => e.IdRace, "monstre_race_idx");

                entity.Property(e => e.IdMonstre).HasColumnName("id_monstre");

                entity.Property(e => e.IdGroupe).HasColumnName("id_groupe");

                entity.Property(e => e.IdRace).HasColumnName("id_race");

                entity.HasOne(d => d.IdGroupeNavigation)
                    .WithMany(p => p.Monstre)
                    .HasForeignKey(d => d.IdGroupe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("monstre_groupe");

                entity.HasOne(d => d.IdRaceNavigation)
                    .WithMany(p => p.Monstre)
                    .HasForeignKey(d => d.IdRace)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("monstre_race");
            });

            modelBuilder.Entity<Origine>(entity =>
            {
                entity.HasKey(e => e.IdOrigine)
                    .HasName("PRIMARY");

                entity.ToTable("origine");

                entity.Property(e => e.IdOrigine).HasColumnName("id_origine");

                entity.Property(e => e.AdresseMax).HasColumnName("adresse_max");

                entity.Property(e => e.AdresseMin).HasColumnName("adresse_min");

                entity.Property(e => e.Bouclier).HasColumnName("bouclier");

                entity.Property(e => e.Charge).HasColumnName("charge");

                entity.Property(e => e.CharismeMax).HasColumnName("charisme_max");

                entity.Property(e => e.CharismeMin).HasColumnName("charisme_min");

                entity.Property(e => e.CourageMax).HasColumnName("courage_max");

                entity.Property(e => e.CourageMin).HasColumnName("courage_min");

                entity.Property(e => e.DeuxMains).HasColumnName("deux_mains");

                entity.Property(e => e.FlairerDanger).HasColumnName("flairer_danger");

                entity.Property(e => e.ForceMax).HasColumnName("force_max");

                entity.Property(e => e.ForceMin).HasColumnName("force_min");

                entity.Property(e => e.IntelligenceMax).HasColumnName("intelligence_max");

                entity.Property(e => e.IntelligenceMin).HasColumnName("intelligence_min");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("nom");

                entity.Property(e => e.PointsResistance).HasColumnName("points_resistance");

                entity.Property(e => e.PvInitial).HasColumnName("pv_initial");
            });

            modelBuilder.Entity<RaceMonstre>(entity =>
            {
                entity.HasKey(e => e.IdRaceMonstre)
                    .HasName("PRIMARY");

                entity.ToTable("race_monstre");

                entity.Property(e => e.IdRaceMonstre).HasColumnName("id_race_monstre");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<TypeItem>(entity =>
            {
                entity.HasKey(e => e.IdTypeItem)
                    .HasName("PRIMARY");

                entity.ToTable("type_item");

                entity.Property(e => e.IdTypeItem).HasColumnName("id_type_item");

                entity.Property(e => e.Nom).HasColumnName("nom");

                entity.Property(e => e.Prix).HasColumnName("prix");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}