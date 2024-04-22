using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CRM_CMC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CRM_CMC.App_Models;
using Microsoft.AspNetCore.Identity;

namespace CRM_CMC.Contexts
{
    public partial class BACKEND_CRMContext : IdentityDbContext<UserInfo, UserRole, string>
    {
        public BACKEND_CRMContext()
        {
        }

        public BACKEND_CRMContext(DbContextOptions<BACKEND_CRMContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; } = null!;
        public virtual DbSet<ChiTietKhoHang> ChiTietKhoHangs { get; set; } = null!;
        public virtual DbSet<HoTroBanHang> HoTroBanHangs { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<KhoHang> KhoHangs { get; set; } = null!;
        public virtual DbSet<LichSuMuaHang> LichSuMuaHangs { get; set; } = null!;
        public virtual DbSet<LichSuTuongTac> LichSuTuongTacs { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<PhanLoaiKhachHang> PhanLoaiKhachHangs { get; set; } = null!;
        public virtual DbSet<QuanLiKhoHang> QuanLiKhoHangs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=BACKEND_CRM;User ID=sa;Password=123;Encrypt=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.ToTable("ChiTietDonHang");

                entity.Property(e => e.ChiTietDonHangId).HasColumnName("ChiTietDonHangID");

                entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LichSuMuaHangId).HasColumnName("LichSuMuaHangID");

                entity.Property(e => e.MaLoaiSanPham).HasMaxLength(30);

                entity.Property(e => e.SoLuongSanPham).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TenSanPham).HasMaxLength(300);

                entity.Property(e => e.Tien1LoaiSanPham).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.LichSuMuaHang)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.LichSuMuaHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichSuMuaHangID_ChiTietDonHang");
            });

            modelBuilder.Entity<ChiTietKhoHang>(entity =>
            {
                entity.ToTable("ChiTietKhoHang");

                entity.HasIndex(e => e.MaSanPham, "UQ__ChiTietK__FAC7442C1CBB82E0")
                    .IsUnique();

                entity.Property(e => e.ChiTietKhoHangId).HasColumnName("ChiTietKhoHangID");

                entity.Property(e => e.HanSuDung).HasColumnType("datetime");

                entity.Property(e => e.MaLoaiSanPham).HasMaxLength(30);

                entity.Property(e => e.MaSanPham).HasMaxLength(30);

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.Property(e => e.NgaySanXuat).HasColumnType("datetime");

                entity.Property(e => e.NgayXuat).HasColumnType("datetime");

                entity.Property(e => e.SoLuong).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TrangThai).HasMaxLength(30);

                entity.HasOne(d => d.MaLoaiSanPhamNavigation)
                    .WithMany(p => p.ChiTietKhoHangs)
                    .HasPrincipalKey(p => p.MaLoaiSanPham)
                    .HasForeignKey(d => d.MaLoaiSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaLoaiSanPham_ChiTietKhoHang");
            });

            modelBuilder.Entity<HoTroBanHang>(entity =>
            {
                entity.ToTable("HoTroBanHang");

                entity.Property(e => e.HoTroBanHangId).HasColumnName("HoTroBanHangID");

                entity.Property(e => e.GiaiDoanBanHang).HasMaxLength(50);

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.SanPhamId).HasColumnName("SanPhamID");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.ToTable("KhachHang");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.LoaiTaiKhoan).HasMaxLength(20);

                entity.Property(e => e.MaTaiKhoan).HasMaxLength(30);

                entity.Property(e => e.MatKhau).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NgayTaoTaiKhoan).HasColumnType("datetime");

                entity.Property(e => e.NhanVienChamSoc).HasMaxLength(200);

                entity.Property(e => e.TenKhachHang).HasMaxLength(30);

                entity.Property(e => e.TenTaiKhoan).HasMaxLength(100);

                entity.Property(e => e.TrangThaiTaiKhoan).HasMaxLength(30);
            });

            modelBuilder.Entity<KhoHang>(entity =>
            {
                entity.ToTable("KhoHang");

                entity.HasIndex(e => e.MaLoaiSanPham, "UQ__KhoHang__ECCF699E05C9017C")
                    .IsUnique();

                entity.Property(e => e.KhoHangId).HasColumnName("KhoHangID");

                entity.Property(e => e.DonVi).HasMaxLength(20);

                entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GiaNhap).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaKhoHang).HasMaxLength(30);

                entity.Property(e => e.MaLoaiSanPham).HasMaxLength(30);

                entity.Property(e => e.NhaCungCap).HasMaxLength(30);

                entity.Property(e => e.TenSanPham).HasMaxLength(30);

                entity.Property(e => e.ThuongHieu).HasMaxLength(30);
            });

            modelBuilder.Entity<LichSuMuaHang>(entity =>
            {
                entity.ToTable("LichSuMuaHang");

                entity.Property(e => e.LichSuMuaHangId).HasColumnName("LichSuMuaHangID");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(20);

                entity.Property(e => e.ThoiGianMua).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.LichSuMuaHangs)
                    .HasForeignKey(d => d.KhachHangId)
                    .HasConstraintName("FK_KhachHangID_LichSuMuaHang");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.LichSuMuaHangs)
                    .HasForeignKey(d => d.NhanVienId)
                    .HasConstraintName("FK_NhanVienID_LichSuMuaHang");
            });

            modelBuilder.Entity<LichSuTuongTac>(entity =>
            {
                entity.ToTable("LichSuTuongTac");

                entity.Property(e => e.LichSuTuongTacId).HasColumnName("LichSuTuongTacID");

                entity.Property(e => e.KetQua).HasMaxLength(200);

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.LoaiTuongTac).HasMaxLength(50);

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.ThoiGianTao).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianTuongTac).HasColumnType("datetime");

                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.LichSuTuongTacs)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KhachHangID_LichSuTuongTac");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.LichSuTuongTacs)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NhanVienID_LichSuTuongTac");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).HasColumnName("locationID");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.ToTable("NhanVien");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.LoaiTaiKhoan).HasMaxLength(20);

                entity.Property(e => e.MaTaiKhoan).HasMaxLength(30);

                entity.Property(e => e.MatKhau).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NgayTaoTaiKhoan).HasColumnType("datetime");

                entity.Property(e => e.TenNhanVien).HasMaxLength(30);

                entity.Property(e => e.TenTaiKhoan).HasMaxLength(100);

                entity.Property(e => e.TrangThaiTaiKhoan).HasMaxLength(30);

                entity.Property(e => e.VaiTro).HasMaxLength(30);
            });

            modelBuilder.Entity<PhanLoaiKhachHang>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PhanLoaiKhachHang");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.HạngMuaHàng)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("Hạng mua hàng");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.LoaiTaiKhoan).HasMaxLength(20);

                entity.Property(e => e.MaTaiKhoan).HasMaxLength(30);

                entity.Property(e => e.MatKhau).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NgayTaoTaiKhoan).HasColumnType("datetime");

                entity.Property(e => e.NhanVienChamSoc).HasMaxLength(200);

                entity.Property(e => e.TenKhachHang).HasMaxLength(30);

                entity.Property(e => e.TenTaiKhoan).HasMaxLength(100);

                entity.Property(e => e.TongChiTieu).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.TrangThaiTaiKhoan).HasMaxLength(30);
            });

            modelBuilder.Entity<QuanLiKhoHang>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("QuanLiKhoHang");

                entity.Property(e => e.DanhSachMaSanPham).HasMaxLength(4000);

                entity.Property(e => e.DonVi).HasMaxLength(20);

                entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GiaNhap).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.KhoHangId).HasColumnName("KhoHangID");

                entity.Property(e => e.MaKhoHang).HasMaxLength(30);

                entity.Property(e => e.MaLoaiSanPham).HasMaxLength(30);

                entity.Property(e => e.NhaCungCap).HasMaxLength(30);

                entity.Property(e => e.TenSanPham).HasMaxLength(30);

                entity.Property(e => e.ThuongHieu).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
