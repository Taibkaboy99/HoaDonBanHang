namespace HoaDonBanHang.Entity
{
    public class KhachHang
    {
        public int KhachHangid { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? SDT { get; set; }
        public IEnumerable<HoaDon>? hoaDons { get; set; }
    }
}
