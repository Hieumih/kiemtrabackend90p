using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblLoaiHinhChamSoc
    {
        public TblLoaiHinhChamSoc()
        {
            TblChamSocKhachHangs = new HashSet<TblChamSocKhachHang>();
        }

        public Guid MaLoaiHinhChamSoc { get; set; }
        public string? TenLoaiHinhChamSoc { get; set; }

        public virtual ICollection<TblChamSocKhachHang> TblChamSocKhachHangs { get; set; }
    }
}
