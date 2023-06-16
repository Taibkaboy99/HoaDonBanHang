namespace HoaDonBanHang.Entity
{
    public class ChiTietHoaDon
    {
        public int ChiTietHoaDonId { get; set; }
        public int HoaDonId { get; set; }
        public HoaDon? HoaDon { get; set; }
        public int SanPhamId { get; set; }
        public SanPham? SanPham { get; set; }
        public int SoLuong { get; set; }
        public string? Donvitinh { get; set; }
        public double ThanhTien { get; set; }
    }
}
