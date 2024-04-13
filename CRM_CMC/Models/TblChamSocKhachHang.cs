using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblChamSocKhachHang
    {
        public Guid MaChamSoc { get; set; }
        public Guid MaKhachHang { get; set; }
        public Guid MaNhanVien { get; set; }
        public DateTime? NgayChamSoc { get; set; }
        public Guid LoaiHinhChamSoc { get; set; }
        public string? NoiDungChamSoc { get; set; }
        public TimeSpan ThoiGianChamSoc { get; set; }

        public virtual TblLoaiHinhChamSoc LoaiHinhChamSocNavigation { get; set; } = null!;
        public virtual TblKhachHang MaKhachHangNavigation { get; set; } = null!;
        public virtual TblNhanVien MaNhanVienNavigation { get; set; } = null!;
    }
}
