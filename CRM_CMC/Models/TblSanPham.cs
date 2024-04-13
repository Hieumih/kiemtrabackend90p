using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblSanPham
    {
        public TblSanPham()
        {
            TblChiTietDonHangs = new HashSet<TblChiTietDonHang>();
        }

        public Guid MaSanPham { get; set; }
        public string TenSanPham { get; set; } = null!;
        public int SoLuongTon { get; set; }
        public decimal DonGia { get; set; }
        public string? MoTa { get; set; }

        public virtual ICollection<TblChiTietDonHang> TblChiTietDonHangs { get; set; }
    }
}
