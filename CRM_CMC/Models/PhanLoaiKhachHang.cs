using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class PhanLoaiKhachHang
    {
        public int KhachHangId { get; set; }
        public string TenKhachHang { get; set; } = null!;
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
        public string? NhanVienChamSoc { get; set; }
        public int? SoLanMuaHang { get; set; }
        public decimal? TongChiTieu { get; set; }
        public string HạngMuaHàng { get; set; } = null!;
    }
}
