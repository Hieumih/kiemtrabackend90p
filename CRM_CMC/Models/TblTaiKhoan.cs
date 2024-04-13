using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblTaiKhoan
    {
        public TblTaiKhoan()
        {
            TblKhachHangs = new HashSet<TblKhachHang>();
            TblNhanViens = new HashSet<TblNhanVien>();
        }

        public Guid MaTaiKhoan { get; set; }
        public string? TenTaiKhoan { get; set; }
        public string? Email { get; set; }
        public int? SoDienThoai { get; set; }
        public string MatKhau { get; set; } = null!;
        public string LoaiTaiKhoan { get; set; } = null!;
        public byte TrangThai { get; set; }
        public string? TokensDanngNhap { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual ICollection<TblKhachHang> TblKhachHangs { get; set; }
        public virtual ICollection<TblNhanVien> TblNhanViens { get; set; }
    }
}
