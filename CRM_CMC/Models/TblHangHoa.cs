using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblHangHoa
    {
        public Guid HhId { get; set; } = Guid.NewGuid();
        public Guid? HhNhhId { get; set; }
        public string? HhMa { get; set; } = string.Empty;
        public string? HhTen { get; set; } = string.Empty;
        public int? HhGianhap { get; set; } = 0;
        public int? HhGiaban { get; set; } = 0;

        public virtual TblNhomhanghoa? HhNhh { get; set; }
    }
}
