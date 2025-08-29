using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetLand> AssetLands { get; set; }

    public virtual DbSet<AssetLandDoc> AssetLandDocs { get; set; }

    public virtual DbSet<AssetLandMap> AssetLandMaps { get; set; }

    public virtual DbSet<Reg_District> Reg_Districts { get; set; }

    public virtual DbSet<Reg_Province> Reg_Provinces { get; set; }

    public virtual DbSet<Reg_Regency> Reg_Regencies { get; set; }

    public virtual DbSet<Reg_Region> Reg_Regions { get; set; }

    public virtual DbSet<Reg_Village> Reg_Villages { get; set; }

    public virtual DbSet<ZRef_AssetAvailability> ZRef_AssetAvailabilities { get; set; }

    public virtual DbSet<ZRef_AssetCategory> ZRef_AssetCategories { get; set; }

    public virtual DbSet<ZRef_AssetCluster> ZRef_AssetClusters { get; set; }

    public virtual DbSet<ZRef_AssetType> ZRef_AssetTypes { get; set; }

    public virtual DbSet<ZRef_CertificateOwner> ZRef_CertificateOwners { get; set; }

    public virtual DbSet<ZRef_CertificateStatus> ZRef_CertificateStatuses { get; set; }

    public virtual DbSet<ZRef_OwnershipType> ZRef_OwnershipTypes { get; set; }

    public virtual DbSet<ZRef_RightsType> ZRef_RightsTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.Property(e => e.Reg_VillageCode).IsFixedLength();

            entity.HasOne(d => d.Reg_VillageCodeNavigation).WithMany(p => p.Assets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_Reg_Village");

            entity.HasOne(d => d.ZRef_AssetAvailability).WithMany(p => p.Assets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_AssetAvailability");

            entity.HasOne(d => d.ZRef_AssetCategory).WithMany(p => p.Assets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_AssetCategory");

            entity.HasOne(d => d.ZRef_AssetCluster).WithMany(p => p.Assets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_AssetCluster");

            entity.HasOne(d => d.ZRef_AssetType).WithMany(p => p.Assets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_AssetType");

            entity.HasOne(d => d.ZRef_OwnershipType).WithMany(p => p.Assets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_OwnershipType");
        });

        modelBuilder.Entity<AssetLand>(entity =>
        {
            entity.Property(e => e.AssetId).ValueGeneratedNever();
            entity.Property(e => e.Value_AppraisalKJPP).IsFixedLength();

            entity.HasOne(d => d.Asset).WithOne(p => p.AssetLand)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetLand_Asset");
        });

        modelBuilder.Entity<AssetLandDoc>(entity =>
        {
            entity.HasOne(d => d.Asset).WithMany(p => p.AssetLandDocs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetLandDoc_AssetLand");

            entity.HasOne(d => d.ZRef_CertificateStatus).WithMany(p => p.AssetLandDocs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetLandDoc_ZRef_CertificateOwner");

            entity.HasOne(d => d.ZRef_CertificateStatusNavigation).WithMany(p => p.AssetLandDocs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetLandDoc_ZRef_CertificateStatus");
        });

       modelBuilder.Entity<AssetLandMap>(entity =>
        {
            entity.HasKey(e => e.AssetId);
            entity.Property(e => e.AssetId).ValueGeneratedNever();

            entity.HasOne(e => e.AssetLand)
                .WithOne(al => al.AssetLandMap)
                .HasForeignKey<AssetLandMap>(e => e.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetLandMap_AssetLand");
        });


        modelBuilder.Entity<Reg_District>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
            entity.Property(e => e.Reg_RegencyCode).IsFixedLength();

            entity.HasOne(d => d.Reg_RegencyCodeNavigation).WithMany(p => p.Reg_Districts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reg_District_Reg_Regency");
        });

        modelBuilder.Entity<Reg_Province>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<Reg_Regency>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
            entity.Property(e => e.Reg_ProvinceCode).IsFixedLength();

            entity.HasOne(d => d.Reg_ProvinceCodeNavigation).WithMany(p => p.Reg_Regencies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reg_Regency_Reg_Province");
        });

        modelBuilder.Entity<Reg_Village>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
            entity.Property(e => e.Reg_DistrictCode).IsFixedLength();
        });

        modelBuilder.Entity<ZRef_AssetCategory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ZRef_AssetCluster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ZRef_AssetType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ZRef_CertificateOwner>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ZRef_CertificateStatus>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ZRef_OwnershipType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ZRef_RightsType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
