using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using backend.Models;

namespace backend.Contexts
{
    public partial class banhangContext : DbContext
    {
        public banhangContext()
        {
        }

        public banhangContext(DbContextOptions<banhangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblHangHoa> TblHangHoas { get; set; } = null!;
        public virtual DbSet<TblNhomhanghoa> TblNhomhanghoas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=banhang;User ID=sa;Password=123;Encrypt=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblHangHoa>(entity =>
            {
                entity.HasKey(e => e.HhId)
                    .HasName("PK__tblHangH__CA9AAAD883D09C6D");

                entity.ToTable("tblHangHoa");

                entity.Property(e => e.HhId)
                    .HasColumnName("HH_ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.HhGiaban).HasColumnName("HH_GIABAN");

                entity.Property(e => e.HhGianhap).HasColumnName("HH_GIANHAP");

                entity.Property(e => e.HhMa)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HH_MA");

                entity.Property(e => e.HhNhhId).HasColumnName("HH_NHH_ID");

                entity.Property(e => e.HhTen)
                    .HasMaxLength(255)
                    .HasColumnName("HH_TEN");

                entity.HasOne(d => d.HhNhh)
                    .WithMany(p => p.TblHangHoas)
                    .HasForeignKey(d => d.HhNhhId)
                    .HasConstraintName("FK__tblHangHo__HH_NH__3B75D760");
            });

            modelBuilder.Entity<TblNhomhanghoa>(entity =>
            {
                entity.HasKey(e => e.NhhId)
                    .HasName("PK__tblNhomh__C3C73916027405FC");

                entity.ToTable("tblNhomhanghoa");

                entity.Property(e => e.NhhId)
                    .HasColumnName("NHH_ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.NhhMa)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NHH_MA");

                entity.Property(e => e.NhhTen)
                    .HasMaxLength(255)
                    .HasColumnName("NHH_TEN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
