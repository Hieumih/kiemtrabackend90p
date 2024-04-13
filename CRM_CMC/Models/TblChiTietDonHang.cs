using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblChiTietDonHang
    {
        public Guid MaChiTietDonHang { get; set; }
        public Guid MaDonHang { get; set; }
        public Guid MaSanPham { get; set; }
        public int SoLuongSanPham { get; set; }
        public decimal DonGia { get; set; }
        public decimal Tong { get; set; }

        public virtual TblDonHang MaDonHangNavigation { get; set; } = null!;
        public virtual TblSanPham MaSanPhamNavigation { get; set; } = null!;
    }
}
