using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Printer.Models;

namespace Printer.DB
{
    public partial class DEContext : DbContext
    {
        public DEContext()
        {
        }

        public DEContext(DbContextOptions<DEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cabinet> Cabinets { get; set; } = null!;
        public virtual DbSet<Claim> Claims { get; set; } = null!;
        public virtual DbSet<ClaimType> ClaimTypes { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<Printer.Models.Type> Types { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=192.168.200.35;database=user04;user=user04;password=93499;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabinet>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cabinet1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Cabinet");
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.ToTable("Claim");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idcabinet).HasColumnName("IDCabinet");

                entity.Property(e => e.IdcalimType).HasColumnName("IDCalimType");

                entity.Property(e => e.IdclaimDevices).HasColumnName("IDClaimDevices");

                entity.Property(e => e.NameOfMatherial)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdcabinetNavigation)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.Idcabinet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claim_Cabinets");

                entity.HasOne(d => d.IdcalimTypeNavigation)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.IdcalimType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claim_ClaimType");

                entity.HasOne(d => d.IdclaimDevicesNavigation)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.IdclaimDevices)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claim_Devices");
            });

            modelBuilder.Entity<ClaimType>(entity =>
            {
                entity.ToTable("ClaimType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdtypeName).HasColumnName("IDTypeName");

                entity.Property(e => e.ModelDevice)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameDevice)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdtypeNameNavigation)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.IdtypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Devices_Type");
            });

            modelBuilder.Entity<Printer.Models.Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        static DEContext instance;
        public static DEContext GetInstance()
        {
            if (instance == null)
                instance = new DEContext();
            return instance;
        }
    }
}
