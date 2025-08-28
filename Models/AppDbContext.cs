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

    public virtual DbSet<RegDistrict> RegDistricts { get; set; }

    public virtual DbSet<RegProvince> RegProvinces { get; set; }

    public virtual DbSet<RegRegency> RegRegencies { get; set; }

    public virtual DbSet<ZrefAssetCategory> ZrefAssetCategories { get; set; }

    public virtual DbSet<ZrefAssetCluster> ZrefAssetClusters { get; set; }

    public virtual DbSet<ZrefAssetType> ZrefAssetTypes { get; set; }

    public virtual DbSet<ZrefCertificateStatus> ZrefCertificateStatuses { get; set; }

    public virtual DbSet<ZrefOwnershipType> ZrefOwnershipTypes { get; set; }

    public virtual DbSet<ZrefRightsType> ZrefRightsTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.ToTable("Asset");

            entity.Property(e => e.CertificateNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CostCenter)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.OwnerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegDistrictId).HasColumnName("Reg_DistrictId");
            entity.Property(e => e.RegProvinceId).HasColumnName("Reg_ProvinceId");
            entity.Property(e => e.RegRegencyId).HasColumnName("Reg_RegencyId");
            entity.Property(e => e.RightsNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TaxObjectNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ValueAcquisition)
                .HasColumnType("money")
                .HasColumnName("Value_Acquisition");
            entity.Property(e => e.ValueBook)
                .HasColumnType("money")
                .HasColumnName("Value_Book");
            entity.Property(e => e.ValueNjop)
                .HasColumnType("money")
                .HasColumnName("Value_NJOP");
            entity.Property(e => e.Village)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ZrefAssetCategoryId).HasColumnName("ZRef_AssetCategoryId");
            entity.Property(e => e.ZrefAssetClusterId).HasColumnName("ZRef_AssetClusterId");
            entity.Property(e => e.ZrefAssetTypeId).HasColumnName("ZRef_AssetTypeId");
            entity.Property(e => e.ZrefCertificateStatusId).HasColumnName("ZRef_CertificateStatusId");
            entity.Property(e => e.ZrefOwnershipTypeId).HasColumnName("ZRef_OwnershipTypeId");
            entity.Property(e => e.ZrefRightsTypeId).HasColumnName("ZRef_RightsTypeId");

            entity.HasOne(d => d.RegDistrict).WithMany(p => p.Assets)
                .HasForeignKey(d => d.RegDistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_Reg_District");

            entity.HasOne(d => d.RegProvince).WithMany(p => p.Assets)
                .HasForeignKey(d => d.RegProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_Reg_Province");

            entity.HasOne(d => d.RegRegency).WithMany(p => p.Assets)
                .HasForeignKey(d => d.RegRegencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_Reg_Regency");

            entity.HasOne(d => d.ZrefAssetCategory).WithMany(p => p.Assets)
                .HasForeignKey(d => d.ZrefAssetCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_AssetCategory");

            entity.HasOne(d => d.ZrefAssetCluster).WithMany(p => p.Assets)
                .HasForeignKey(d => d.ZrefAssetClusterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_AssetCluster");

            entity.HasOne(d => d.ZrefAssetType).WithMany(p => p.Assets)
                .HasForeignKey(d => d.ZrefAssetTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_AssetType");

            entity.HasOne(d => d.ZrefCertificateStatus).WithMany(p => p.Assets)
                .HasForeignKey(d => d.ZrefCertificateStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_CertificateStatus");

            entity.HasOne(d => d.ZrefOwnershipType).WithMany(p => p.Assets)
                .HasForeignKey(d => d.ZrefOwnershipTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_OwnershipType");

            entity.HasOne(d => d.ZrefRightsType).WithMany(p => p.Assets)
                .HasForeignKey(d => d.ZrefRightsTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_ZRef_RightsType");
        });

        modelBuilder.Entity<RegDistrict>(entity =>
        {
            entity.ToTable("Reg_District", "Region");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegRegencyId).HasColumnName("Reg_RegencyId");

            entity.HasOne(d => d.RegRegency).WithMany(p => p.RegDistricts)
                .HasForeignKey(d => d.RegRegencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reg_District_Reg_Regency");
        });

        modelBuilder.Entity<RegProvince>(entity =>
        {
            entity.ToTable("Reg_Province", "Region");

            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RegRegency>(entity =>
        {
            entity.ToTable("Reg_Regency", "Region");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegProvinceId).HasColumnName("Reg_ProvinceId");

            entity.HasOne(d => d.RegProvince).WithMany(p => p.RegRegencies)
                .HasForeignKey(d => d.RegProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reg_Regency_Reg_Province");
        });

        modelBuilder.Entity<ZrefAssetCategory>(entity =>
        {
            entity.ToTable("ZRef_AssetCategory");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZrefAssetCluster>(entity =>
        {
            entity.ToTable("ZRef_AssetCluster");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZrefAssetType>(entity =>
        {
            entity.ToTable("ZRef_AssetType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZrefCertificateStatus>(entity =>
        {
            entity.ToTable("ZRef_CertificateStatus");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZrefOwnershipType>(entity =>
        {
            entity.ToTable("ZRef_OwnershipType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZrefRightsType>(entity =>
        {
            entity.ToTable("ZRef_RightsType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
