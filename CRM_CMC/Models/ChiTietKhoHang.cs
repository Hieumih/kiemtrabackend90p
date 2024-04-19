using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class ChiTietKhoHang
    {
        public int ChiTietKhoHangId { get; set; }
        public string MaLoaiSanPham { get; set; } = null!;
        public string? MaSanPham { get; set; }
        public decimal? SoLuong { get; set; }
        public string? TrangThai { get; set; }
        public DateTime? NgayNhap { get; set; }
        public DateTime? NgayXuat { get; set; }
        public DateTime? NgaySanXuat { get; set; }
        public DateTime? HanSuDung { get; set; }

        public virtual KhoHang MaLoaiSanPhamNavigation { get; set; } = null!;
    }
}
