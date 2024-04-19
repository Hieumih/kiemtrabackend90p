using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class KhoHang
    {
        public KhoHang()
        {
            ChiTietKhoHangs = new HashSet<ChiTietKhoHang>();
        }

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

        public virtual ICollection<ChiTietKhoHang> ChiTietKhoHangs { get; set; }
    }
}
