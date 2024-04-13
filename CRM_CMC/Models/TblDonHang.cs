using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblDonHang
    {
        public TblDonHang()
        {
            TblChiTietDonHangs = new HashSet<TblChiTietDonHang>();
        }

        public Guid MaDonHang { get; set; }
        public Guid MaKhachHang { get; set; }
        public Guid MaNhanVien { get; set; }
        public DateTime NgayDatHang { get; set; }
        public decimal TongTien { get; set; }

        public virtual TblKhachHang MaKhachHangNavigation { get; set; } = null!;
        public virtual TblNhanVien MaNhanVienNavigation { get; set; } = null!;
        public virtual ICollection<TblChiTietDonHang> TblChiTietDonHangs { get; set; }
    }
}
