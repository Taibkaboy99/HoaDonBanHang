using HoaDonBanHang.Entity;
using HoaDonBanHang.Helper;

namespace HoaDonBanHang.IService
{
    public interface IHoaDonService
    {
        public HoaDon ThemHoaDon(HoaDon hoadon);
        public string TaoMaGiaDich();
        public HoaDon SuaHoaDon(int HoadonId,HoaDon hoadon);
        public void XoaHoaDon(int HoadonId);
        public IQueryable<HoaDon> LayHoaDon(string? keyword,
                                            int? year = null,
                                            int? thang = null,
                                            DateTime? tungay = null,
                                            DateTime? denngay = null,
                                            int? giatu = null,
                                            int? dengia = null,
                                            Pagination pagination = null
                                            
            );
    }
}
