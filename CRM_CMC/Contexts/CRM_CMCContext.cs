using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CRM_CMC.Models;

namespace CRM_CMC.Contexts
{
    public partial class CRM_CMCContext : DbContext
    {
        public CRM_CMCContext()
        {
        }

        public CRM_CMCContext(DbContextOptions<CRM_CMCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblChamSocKhachHang> TblChamSocKhachHangs { get; set; } = null!;
        public virtual DbSet<TblChiTietDonHang> TblChiTietDonHangs { get; set; } = null!;
        public virtual DbSet<TblDonHang> TblDonHangs { get; set; } = null!;
        public virtual DbSet<TblKhachHang> TblKhachHangs { get; set; } = null!;
        public virtual DbSet<TblLichLamViec> TblLichLamViecs { get; set; } = null!;
        public virtual DbSet<TblLoaiHinhChamSoc> TblLoaiHinhChamSocs { get; set; } = null!;
        public virtual DbSet<TblLocation> TblLocations { get; set; } = null!;
        public virtual DbSet<TblNguonKhachHang> TblNguonKhachHangs { get; set; } = null!;
        public virtual DbSet<TblNhanVien> TblNhanViens { get; set; } = null!;
        public virtual DbSet<TblRoleHeThong> TblRoleHeThongs { get; set; } = null!;
        public virtual DbSet<TblSanPham> TblSanPhams { get; set; } = null!;
        public virtual DbSet<TblThamGium> TblThamGia { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=CRM_CMC;User ID=sa;Password=123;Encrypt=True;Trust Server Certificate=True");
                optionsBuilder.UseSqlite("Data Source=CRM.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblChamSocKhachHang>(entity =>
            {
                entity.HasKey(e => e.MaChamSoc)
                    .HasName("PK__tblChamS__5F61886460CDBF8A");

                entity.ToTable("tblChamSocKhachHang");

                entity.Property(e => e.MaChamSoc).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NgayChamSoc)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.LoaiHinhChamSocNavigation)
                    .WithMany(p => p.TblChamSocKhachHangs)
                    .HasForeignKey(d => d.LoaiHinhChamSoc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoaiHinhChamSoc_ChamSocKhachHang");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.TblChamSocKhachHangs)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaKhachHang_ChamSocKhachHang");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.TblChamSocKhachHangs)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaNhanVien_ChamSocKhachHang");
            });

            modelBuilder.Entity<TblChiTietDonHang>(entity =>
            {
                entity.HasKey(e => e.MaChiTietDonHang)
                    .HasName("PK__tblChiTi__4B0B45DD2AB9E02D");

                entity.ToTable("tblChiTietDonHang");

                entity.Property(e => e.MaChiTietDonHang).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Tong).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MaDonHangNavigation)
                    .WithMany(p => p.TblChiTietDonHangs)
                    .HasForeignKey(d => d.MaDonHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaDonHang_ChiTietDonHang");

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.TblChiTietDonHangs)
                    .HasForeignKey(d => d.MaSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaSanPham_ChiTietDonHang");
            });

            modelBuilder.Entity<TblDonHang>(entity =>
            {
                entity.HasKey(e => e.MaDonHang)
                    .HasName("PK__tblDonHa__129584AD24672E73");

                entity.ToTable("tblDonHang");

                entity.Property(e => e.MaDonHang).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NgayDatHang)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.TblDonHangs)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaKhachHang_DonHang");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.TblDonHangs)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaNhanVien_DonHang");
            });

            modelBuilder.Entity<TblKhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__tblKhach__88D2F0E5AB59EF3E");

                entity.ToTable("tblKhachHang");

                entity.Property(e => e.MaKhachHang).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.LoaiTaiKhoan)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);

                entity.Property(e => e.TenKhachHang).HasMaxLength(255);

                entity.Property(e => e.TenTaiKhoan)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TokensDanngNhap)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tokensDanngNhap");

                entity.HasOne(d => d.NhanVienTakeCareNavigation)
                    .WithMany(p => p.TblKhachHangs)
                    .HasForeignKey(d => d.NhanVienTakeCare)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NhanVienTakeCare_KhachHang");
            });

            modelBuilder.Entity<TblLichLamViec>(entity =>
            {
                entity.HasKey(e => e.MaLichLamViec)
                    .HasName("PK__tblLichL__22FDA272D4E9B99F");

                entity.ToTable("tblLichLamViec");

                entity.Property(e => e.MaLichLamViec).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenCongViec).HasMaxLength(255);

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.TblLichLamViecs)
                    .HasForeignKey(d => d.MaKhachHang)
                    .HasConstraintName("fk_MaKhachHang_tblLichLamViec");
            });

            modelBuilder.Entity<TblLoaiHinhChamSoc>(entity =>
            {
                entity.HasKey(e => e.MaLoaiHinhChamSoc)
                    .HasName("PK__tblLoaiH__D3E43FE828E1D11F");

                entity.ToTable("tblLoaiHinhChamSoc");

                entity.Property(e => e.MaLoaiHinhChamSoc).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TenLoaiHinhChamSoc).HasMaxLength(255);
            });

            modelBuilder.Entity<TblLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__tblLocat__30646B0E868EFEC5");

                entity.ToTable("tblLocation");

                entity.Property(e => e.LocationId).HasColumnName("locationID");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TblNguonKhachHang>(entity =>
            {
                entity.HasKey(e => e.MaNguonKhachHang)
                    .HasName("PK__tblNguon__A3C705A8EB4B28B2");

                entity.ToTable("tblNguonKhachHang");

                entity.Property(e => e.MaNguonKhachHang).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NgaySua).HasColumnType("date");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenNguonKhachHang).HasMaxLength(50);

                entity.HasOne(d => d.NguoiSuaNavigation)
                    .WithMany(p => p.TblNguonKhachHangNguoiSuaNavigations)
                    .HasForeignKey(d => d.NguoiSua)
                    .HasConstraintName("FK_NguoiSua_tblNguonKhachHang");

                entity.HasOne(d => d.NguoiTaoNavigation)
                    .WithMany(p => p.TblNguonKhachHangNguoiTaoNavigations)
                    .HasForeignKey(d => d.NguoiTao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NguoiTao_tblNguonKhachHang");
            });

            modelBuilder.Entity<TblNhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien)
                    .HasName("PK__tblNhanV__77B2CA47AEFFCA0A");

                entity.ToTable("tblNhanVien");

                entity.Property(e => e.MaNhanVien).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.LoaiTaiKhoan)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);

                entity.Property(e => e.TenNhanVien).HasMaxLength(255);

                entity.Property(e => e.TokensDanngNhap)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tokensDanngNhap");

                entity.Property(e => e.VaiTro).HasMaxLength(255);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.TblNhanViens)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Role_tblNhanVien");
            });

            modelBuilder.Entity<TblRoleHeThong>(entity =>
            {
                entity.HasKey(e => e.MaRole)
                    .HasName("PK__tblRoleH__0639A0FDFF578FD3");

                entity.ToTable("tblRoleHeThong");

                entity.Property(e => e.MaRole).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TenQuyen).HasMaxLength(255);
            });

            modelBuilder.Entity<TblSanPham>(entity =>
            {
                entity.HasKey(e => e.MaSanPham)
                    .HasName("PK__tblSanPh__FAC7442DA9E7BF46");

                entity.ToTable("tblSanPham");

                entity.Property(e => e.MaSanPham).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TenSanPham).HasMaxLength(255);
            });

            modelBuilder.Entity<TblThamGium>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblThamGia");

                entity.HasIndex(e => e.MaLichLamViec, "UQ__tblThamG__22FDA273542B8980")
                    .IsUnique();

                entity.HasIndex(e => e.MaNhanVien, "UQ__tblThamG__77B2CA46BB66E407")
                    .IsUnique();

                entity.HasOne(d => d.MaLichLamViecNavigation)
                    .WithOne()
                    .HasForeignKey<TblThamGium>(d => d.MaLichLamViec)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MaLichLamViec_tblThamGia");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithOne()
                    .HasForeignKey<TblThamGium>(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MaNhanVien_tblThamGia");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
