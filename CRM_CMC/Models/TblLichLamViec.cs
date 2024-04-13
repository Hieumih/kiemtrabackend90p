using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblLichLamViec
    {
        public Guid MaLichLamViec { get; set; }
        public string TenCongViec { get; set; } = null!;
        public Guid? MaKhachHang { get; set; }
        public string Mota { get; set; } = null!;
        public DateTime NgayTao { get; set; }
        public Guid NguoiTao { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public virtual TblKhachHang? MaKhachHangNavigation { get; set; }
    }
}
