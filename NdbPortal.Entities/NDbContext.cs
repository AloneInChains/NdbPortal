using System;
using NdbPortal.Entities.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NdbPortal.Entities
{
    public partial class NDbContext : DbContext
    {
        public NDbContext()
        {
        }

        public NDbContext(DbContextOptions<NDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<NormativeDocument> NormativeDocuments { get; set; } = null!;
        public virtual DbSet<NormativeDocumentConfidentialityLevel> NormativeDocumentConfidentialityLevels { get; set; } = null!;
        public virtual DbSet<NormativeDocumentFile> NormativeDocumentFiles { get; set; } = null!;
        public virtual DbSet<NormativeDocumentRelation> NormativeDocumentRelations { get; set; } = null!;
        public virtual DbSet<NormativeDocumentRelationType> NormativeDocumentRelationTypes { get; set; } = null!;
        public virtual DbSet<NormativeDocumentType> NormativeDocumentTypes { get; set; } = null!;
        public virtual DbSet<NormativeDocumentVisa> NormativeDocumentVisas { get; set; } = null!;
        public virtual DbSet<VwNormativeDocumentRelation> VwNormativeDocumentRelations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_CompanyId");

                entity.HasOne(d => d.ConfidentialityLevel)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ConfidentialityLevelId)
                    .HasConstraintName("FK_Employee_ConfidentialityLevelId");
            });

            modelBuilder.Entity<NormativeDocument>(entity =>
            {
                entity.ToTable("NormativeDocument");

                entity.HasIndex(e => e.CreatedOn, "IDX_NormativeDocument_CreatedOn");

                entity.HasIndex(e => e.DocumentNumber, "IDX_NormativeDocument_DocumentNumber");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.NormativeDocuments)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NormativeDocument_CompanyId");

                entity.HasOne(d => d.ConfidentialityLevel)
                    .WithMany(p => p.NormativeDocuments)
                    .HasForeignKey(d => d.ConfidentialityLevelId)
                    .HasConstraintName("FK_NormativeDocument_ConfidentialityLevelId");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.NormativeDocuments)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NormativeDocument_CreatedById");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.NormativeDocuments)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("FK_NormativeDocument_DocumentTypeId");

                entity.HasOne(d => d.MainDocument)
                    .WithMany(p => p.InverseMainDocument)
                    .HasForeignKey(d => d.MainDocumentId)
                    .HasConstraintName("FK_NormativeDocument_MainDocumentId");
            });

            modelBuilder.Entity<NormativeDocumentConfidentialityLevel>(entity =>
            {
                entity.ToTable("NormativeDocumentConfidentialityLevel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                    .HasDefaultValue(1);
            });

            modelBuilder.Entity<NormativeDocumentFile>(entity =>
            {
                entity.ToTable("NormativeDocumentFile");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.NormativeDocumentFiles)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NormativeDocumentFile_CreatedById");
            });

            modelBuilder.Entity<NormativeDocumentRelation>(entity =>
            {
                entity.ToTable("NormativeDocumentRelation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.RelatedDocument)
                    .WithMany(p => p.NormativeDocumentRelationRelatedDocuments)
                    .HasForeignKey(d => d.RelatedDocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NormativeDocumentRelation_RelatedDocumentId");

                entity.HasOne(d => d.RelationDocument)
                    .WithMany(p => p.NormativeDocumentRelationRelationDocuments)
                    .HasForeignKey(d => d.RelationDocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NormativeDocumentRelation_RelationDocumentId");

                entity.HasOne(d => d.RelationType)
                    .WithMany(p => p.NormativeDocumentRelations)
                    .HasForeignKey(d => d.RelationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NormativeDocumentRelation_RelationTypeId");
            });

            modelBuilder.Entity<NormativeDocumentRelationType>(entity =>
            {
                entity.ToTable("NormativeDocumentRelationType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReverseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NormativeDocumentType>(entity =>
            {
                entity.ToTable("NormativeDocumentType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NormativeDocumentVisa>(entity =>
            {
                entity.ToTable("NormativeDocumentVisa");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.NormativeDocumentVisas)
                    .HasForeignKey(d => d.ApproverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NormativeDocumentVisa_ApproverId");

                entity.HasOne(d => d.NormativeDocument)
                    .WithMany(p => p.NormativeDocumentVisas)
                    .HasForeignKey(d => d.NormativeDocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NormativeDocumentVisa_NormativeDocumentId");
            });

            modelBuilder.Entity<VwNormativeDocumentRelation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwNormativeDocumentRelation");

                entity.Property(e => e.RelationName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
