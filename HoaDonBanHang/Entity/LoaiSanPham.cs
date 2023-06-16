namespace HoaDonBanHang.Entity
{
    public class LoaiSanPham
    {
        public int LoaiSanPhamID { get; set; }
        public string? Tenloaisanpham { get; set; }
        public IEnumerable<SanPham>? sanPhams { get; set; }
    }
}
