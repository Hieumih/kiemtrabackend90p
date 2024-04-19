using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class LichSuTuongTac
    {
        public int LichSuTuongTacId { get; set; }
        public int KhachHangId { get; set; }
        public int NhanVienId { get; set; }
        public string? DanhSachMaSanPham { get; set; }
        public string? LoaiTuongTac { get; set; }
        public DateTime? ThoiGianTuongTac { get; set; }
        public string? NoiDungTuongTac { get; set; }
        public string? KetQua { get; set; }
        public string? GhiChu { get; set; }
        public string? TrangThai { get; set; }
        public DateTime? ThoiGianTao { get; set; }

        public virtual KhachHang KhachHang { get; set; } = null!;
        public virtual NhanVien NhanVien { get; set; } = null!;
    }
}
