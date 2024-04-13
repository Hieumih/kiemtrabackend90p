using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblNguonKhachHang
    {
        public Guid MaNguonKhachHang { get; set; }
        public string? TenNguonKhachHang { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayTao { get; set; }
        public Guid NguoiTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public Guid? NguoiSua { get; set; }

        public virtual TblNhanVien? NguoiSuaNavigation { get; set; }
        public virtual TblNhanVien NguoiTaoNavigation { get; set; } = null!;
    }
}
