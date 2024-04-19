using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class HoTroBanHang
    {
        public int HoTroBanHangId { get; set; }
        public Guid KhachHangId { get; set; }
        public int SanPhamId { get; set; }
        public string GiaiDoanBanHang { get; set; } = null!;
    }
}
