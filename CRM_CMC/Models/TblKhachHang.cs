using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class TblKhachHang
    {
        public TblKhachHang()
        {
            TblChamSocKhachHangs = new HashSet<TblChamSocKhachHang>();
            TblDonHangs = new HashSet<TblDonHang>();
            TblLichLamViecs = new HashSet<TblLichLamViec>();
        }

        public Guid MaKhachHang { get; set; }
        public Guid? MaTaiKhoan { get; set; }
        public string TenKhachHang { get; set; } = null!;
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; } = null!;
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string DiaChi { get; set; } = null!;
        public Guid NhanVienTakeCare { get; set; }
        public string? TenTaiKhoan { get; set; }
        public string MatKhau { get; set; } = null!;
        public string LoaiTaiKhoan { get; set; } = null!;
        public byte TrangThai { get; set; }
        public string? TokensDanngNhap { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual TblNhanVien NhanVienTakeCareNavigation { get; set; } = null!;
        public virtual ICollection<TblChamSocKhachHang> TblChamSocKhachHangs { get; set; }
        public virtual ICollection<TblDonHang> TblDonHangs { get; set; }
        public virtual ICollection<TblLichLamViec> TblLichLamViecs { get; set; }
    }
}
