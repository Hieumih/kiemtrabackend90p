using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblRoleHeThong
    {
        public TblRoleHeThong()
        {
            TblNhanViens = new HashSet<TblNhanVien>();
        }

        public Guid MaRole { get; set; }
        public string TenQuyen { get; set; } = null!;
        public string? Mota { get; set; }

        public virtual ICollection<TblNhanVien> TblNhanViens { get; set; }
    }
}
