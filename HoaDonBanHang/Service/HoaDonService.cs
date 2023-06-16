using HoaDonBanHang.Entity;
using HoaDonBanHang.Helper;
using HoaDonBanHang.IService;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace HoaDonBanHang.Service
{
    public class HoaDonService : IHoaDonService
    {
        private readonly AppDbContext DbContext;
        public HoaDonService()
        {
            DbContext = new AppDbContext();
        }

        public IQueryable<HoaDon> LayHoaDon(string? keyword,int? year = null, int? thang = null,
                                            DateTime? tungay = null,
                                            DateTime? denngay = null,
                                            int? giatu = null,
                                            int? dengia = null,
                                            Pagination pagination = null
                                            )
        {
            var query = DbContext.HoaDons.Include(x => x.chiTietHoaDons)
                                         .OrderByDescending(x => x.Thoigiantao)
                                         .AsQueryable();
            
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.TenHoaDon.ToLower().Contains(keyword.ToLower())
                                         ||x.MaGiaoDich.ToLower().Contains(keyword.ToLower()));
            }
            if (year.HasValue)
            {
                query = query.Where(x => x.Thoigiantao.Year == year.Value);
            }
            if (thang.HasValue)
            {
                query = query.Where(x => x.Thoigiantao.Month == thang.Value);
            }
            if (tungay.HasValue)
            {
                query = query.Where(x => x.Thoigiantao.Date >= tungay.Value.Date);
            }
            if (denngay.HasValue)
            {
                query = query.Where(x => x.Thoigiantao.Date <= denngay.Value.Date);
            }
            if (giatu.HasValue)
            {
                query = query.Where(x => x.TongTien >= giatu);
            }
            if (dengia.HasValue)
            {
                query = query.Where(x => x.TongTien <= dengia);
            }
            return query;
        }

        public HoaDon SuaHoaDon(int HoadonId, HoaDon hoadon)
        {
            if (DbContext.HoaDons.Any(x => x.HoaDonId == HoadonId))
            {
                using (var trans = DbContext.Database.BeginTransaction())
                {
                    if (hoadon.chiTietHoaDons == null || hoadon.chiTietHoaDons.Count() == 0)
                    {
                        var lstCTHDHienTai = DbContext.ChiTietHoaDons.Where(x => x.HoaDonId == hoadon.HoaDonId);
                        DbContext.RemoveRange(lstCTHDHienTai);
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        var lstCTHDHienTai = DbContext.ChiTietHoaDons.Where(x => x.HoaDonId == hoadon.HoaDonId);
                        var lstCTHDDelete = new List<ChiTietHoaDon>();
                        foreach (var chitiet in lstCTHDHienTai)
                        {
                            if (!hoadon.chiTietHoaDons.Any(x => x.ChiTietHoaDonId == chitiet.ChiTietHoaDonId))
                            {
                                lstCTHDDelete.Add(chitiet);
                            }
                            else
                            {
                                var chiTietMoi = hoadon.chiTietHoaDons.FirstOrDefault(x => x.ChiTietHoaDonId == chitiet.ChiTietHoaDonId);
                                chitiet.HoaDonId = hoadon.HoaDonId;
                                chitiet.SanPhamId = chiTietMoi.SanPhamId;
                                chitiet.SoLuong = chiTietMoi.SoLuong;
                                chitiet.Donvitinh = chiTietMoi.Donvitinh;
                                var Sanpham = DbContext.SanPhams.FirstOrDefault(z => z.SanPhamId == chitiet.SanPhamId);
                                chitiet.ThanhTien = Sanpham.GiaThanh * chiTietMoi.SoLuong;
                                DbContext.Update(chitiet);
                                DbContext.SaveChanges();
                            }
                        }
                        DbContext.RemoveRange(lstCTHDDelete);
                        DbContext.SaveChanges();
                        foreach (var chiTiet in hoadon.chiTietHoaDons)
                        {
                            if (!lstCTHDHienTai.Any(x => x.ChiTietHoaDonId == chiTiet.ChiTietHoaDonId))
                            {
                                chiTiet.HoaDonId = hoadon.HoaDonId;
                                var sanPham = DbContext.SanPhams.FirstOrDefault(y => y.SanPhamId == chiTiet.SanPhamId);
                                chiTiet.ThanhTien = sanPham.GiaThanh * chiTiet.SoLuong;
                                DbContext.Update(chiTiet);
                                DbContext.SaveChanges();
                            }
                        }
                    }
                    var tongTienMoi = DbContext.ChiTietHoaDons.Where(x => x.HoaDonId == hoadon.HoaDonId).Sum(x => x.ThanhTien);
                    hoadon.TongTien = tongTienMoi;
                    hoadon.Thoigiancapnhat = DateTime.Now;
                    hoadon.chiTietHoaDons = null;
                    DbContext.Update(hoadon);
                    DbContext.SaveChanges();
                    trans.Commit();
                    return hoadon;
                }
            }
            else throw new Exception("Hoa don chua ton tai!");
        }

        public string TaoMaGiaDich()
        {
            var res = DateTime.Now.ToString("yyyyMMdd") + "_";
            var countSoGiaoDichhomnay = DbContext.HoaDons.Count(x => x.Thoigiantao.Date == DateTime.Now.Date);
            if (countSoGiaoDichhomnay > 0)
            {
                int temp = countSoGiaoDichhomnay + 1;
                if (countSoGiaoDichhomnay + 1 < 10) return res = res + "00" + temp;
                else if (temp < 100) return res = res + "0" + temp.ToString();
                else return res = res + temp.ToString();
            }
            else return res + "001";
        }

        public HoaDon ThemHoaDon(HoaDon hoadon)
        {
                using (var trans = DbContext.Database.BeginTransaction())
                {
                    hoadon.Thoigiantao = DateTime.Now;
                    hoadon.MaGiaoDich = TaoMaGiaDich();
                    //hoadon.Khachhangid = 1;
                    //var checkkh = DbContext.KhachHangs.FirstOrDefault(x => x.KhachHangid == hoadon.Khachhangid);
                    var LstChitiethoadon = hoadon.chiTietHoaDons;
                    hoadon.chiTietHoaDons = null;
                    DbContext.HoaDons.Add(hoadon);
                    DbContext.SaveChanges();
                    foreach (var chitiet in LstChitiethoadon)
                    {
                        var checksanpham = DbContext.SanPhams.FirstOrDefault(x => x.SanPhamId == chitiet.SanPhamId);
                        if (checksanpham != null)
                        {
                            chitiet.HoaDonId = hoadon.HoaDonId;
                            chitiet.SanPham = checksanpham;
                            chitiet.ThanhTien = chitiet.SoLuong * checksanpham.GiaThanh;
                            DbContext.ChiTietHoaDons.Add(chitiet);
                            DbContext.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("San pham chua ton tai.Vui long kiem tra lai!");
                        }
                    }
                    hoadon.TongTien = LstChitiethoadon.Sum(x => x.ThanhTien);
                    DbContext.Update(hoadon);
                    DbContext.SaveChanges();
                    trans.Commit();
                }
            return hoadon;
        }

        public void XoaHoaDon(int HoadonId)
        {
            if (DbContext.HoaDons.Any(x => x.HoaDonId == HoadonId))
            {
                using (var trans = DbContext.Database.BeginTransaction())
                {
                    var lstCTHDHienTai = DbContext.ChiTietHoaDons.Where(x => x.HoaDonId == HoadonId);
                    DbContext.RemoveRange(lstCTHDHienTai);
                    DbContext.SaveChanges();
                    var hoaDon = DbContext.HoaDons.Find(HoadonId);
                    DbContext.Remove(hoaDon);
                    DbContext.SaveChanges();
                    trans.Commit();
                }
            }
            else throw new Exception("Hoa don khong ton tai!");
        }

    }
}
