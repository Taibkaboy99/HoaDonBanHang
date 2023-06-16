using HoaDonBanHang.Entity;
using Microsoft.EntityFrameworkCore;

namespace HoaDonBanHang
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-QPOO44O\\DANGTAI;Database = HoaDonS;Trusted_Connection = True;TrustServerCertificate=True;");
        }
    }
}
