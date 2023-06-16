namespace HoaDonBanHang.Entity
{
    public class HoaDon
    {
        public int HoaDonId { get; set; }
        public int? Khachhangid { get; set; }
        public KhachHang? KhachHang { get; set; }
        public string? TenHoaDon { get; set; }
        public string? MaGiaoDich { get; set; }
        public DateTime Thoigiantao { get; set; }
        public DateTime? Thoigiancapnhat { get; set; }
        public string? GhiChu { get; set; }
        public double? TongTien { get; set; }
        public IEnumerable<ChiTietHoaDon>? chiTietHoaDons { get; set; }
    }
}
