using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class QuanLiKhoHang
    {
        public int KhoHangId { get; set; }
        public string? MaKhoHang { get; set; }
        public string MaLoaiSanPham { get; set; } = null!;
        public string TenSanPham { get; set; } = null!;
        public string? MoTa { get; set; }
        public decimal GiaBan { get; set; }
        public decimal? GiaNhap { get; set; }
        public string? NhaCungCap { get; set; }
        public string? ThuongHieu { get; set; }
        public string? DonVi { get; set; }
        public string? DanhSachMaSanPham { get; set; }
        public int? SoLuongNhap { get; set; }
        public int? SoLuongTon { get; set; }
        public int? SoLuongXuat { get; set; }
    }
}
