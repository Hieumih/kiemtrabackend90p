using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            LichSuMuaHangs = new HashSet<LichSuMuaHang>();
            LichSuTuongTacs = new HashSet<LichSuTuongTac>();
        }

        public int NhanVienId { get; set; }
        public string TenNhanVien { get; set; } = null!;
        public string? DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? MaTaiKhoan { get; set; }
        public string? TenTaiKhoan { get; set; }
        public string? Email { get; set; }
        public int? SoDienThoai { get; set; }
        public string? MatKhau { get; set; }
        public string? LoaiTaiKhoan { get; set; }
        public string? TrangThaiTaiKhoan { get; set; }
        public Guid? TokenDangNhap { get; set; }
        public DateTime? NgayTaoTaiKhoan { get; set; }
        public string? VaiTro { get; set; }
        public Guid Role { get; set; }

        public virtual ICollection<LichSuMuaHang> LichSuMuaHangs { get; set; }
        public virtual ICollection<LichSuTuongTac> LichSuTuongTacs { get; set; }
    }
}
