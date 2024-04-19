using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class ChiTietDonHang
    {
        public int ChiTietDonHangId { get; set; }
        public int LichSuMuaHangId { get; set; }
        public string MaLoaiSanPham { get; set; } = null!;
        public string DanhSachMaSanPham { get; set; } = null!;
        public string TenSanPham { get; set; } = null!;
        public decimal GiaBan { get; set; }
        public decimal SoLuongSanPham { get; set; }
        public decimal Tien1LoaiSanPham { get; set; }

        public virtual LichSuMuaHang LichSuMuaHang { get; set; } = null!;
    }
}
