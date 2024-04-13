using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblThamGium
    {
        public Guid MaLichLamViec { get; set; }
        public Guid MaNhanVien { get; set; }

        public virtual TblLichLamViec MaLichLamViecNavigation { get; set; } = null!;
        public virtual TblNhanVien MaNhanVienNavigation { get; set; } = null!;
    }
}
