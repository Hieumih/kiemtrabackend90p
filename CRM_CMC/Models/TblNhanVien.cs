using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblNhanVien
    {
        public TblNhanVien()
        {
            TblChamSocKhachHangs = new HashSet<TblChamSocKhachHang>();
            TblDonHangs = new HashSet<TblDonHang>();
            TblKhachHangs = new HashSet<TblKhachHang>();
            TblNguonKhachHangNguoiSuaNavigations = new HashSet<TblNguonKhachHang>();
            TblNguonKhachHangNguoiTaoNavigations = new HashSet<TblNguonKhachHang>();
        }

        public Guid MaNhanVien { get; set; }
        public Guid MaTaiKhoan { get; set; }
        public string VaiTro { get; set; } = null!;
        public string TenNhanVien { get; set; } = null!;
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string SoDienThoai { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string LoaiTaiKhoan { get; set; } = null!;
        public byte TrangThai { get; set; }
        public string? TokensDanngNhap { get; set; }
        public DateTime NgayTao { get; set; }
        public Guid Role { get; set; }

        public virtual TblRoleHeThong RoleNavigation { get; set; } = null!;
        public virtual ICollection<TblChamSocKhachHang> TblChamSocKhachHangs { get; set; }
        public virtual ICollection<TblDonHang> TblDonHangs { get; set; }
        public virtual ICollection<TblKhachHang> TblKhachHangs { get; set; }
        public virtual ICollection<TblNguonKhachHang> TblNguonKhachHangNguoiSuaNavigations { get; set; }
        public virtual ICollection<TblNguonKhachHang> TblNguonKhachHangNguoiTaoNavigations { get; set; }
    }
}
