namespace HoaDonBanHang.Entity
{
    public class SanPham
    {
        public int SanPhamId { get; set; }
        public int LoaisanphamId { get; set; }
        public LoaiSanPham? LoaiSanPham { get; set; }
        public string? TenSanPham { get; set; }
        public double GiaThanh { get; set; }
        public string? MoTa { get; set; }
        public DateTime? Ngayhethan { get; set; }
        public string? KyHieuSanPham { get; set; }
        public IEnumerable<ChiTietHoaDon>? chiTietHoaDons { get; set; }
    }
}
