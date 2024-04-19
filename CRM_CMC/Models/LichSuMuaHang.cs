using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class LichSuMuaHang
    {
        public LichSuMuaHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public int LichSuMuaHangId { get; set; }
        public int? KhachHangId { get; set; }
        public int? NhanVienId { get; set; }
        public decimal? TongTien { get; set; }
        public DateTime? ThoiGianMua { get; set; }
        public string? PhuongThucThanhToan { get; set; }

        public virtual KhachHang? KhachHang { get; set; }
        public virtual NhanVien? NhanVien { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
