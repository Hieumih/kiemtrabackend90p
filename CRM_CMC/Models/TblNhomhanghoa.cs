using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblNhomhanghoa
    {
        public TblNhomhanghoa()
        {
            TblHangHoas = new HashSet<TblHangHoa>();
        }

        public Guid NhhId { get; set; } = Guid.NewGuid();
        public string? NhhMa { get; set; } = string.Empty;
        public string? NhhTen { get; set; } = string.Empty;

        public virtual ICollection<TblHangHoa> TblHangHoas { get; set; }
    }
}
