using HoaDonBanHang.Entity;
using HoaDonBanHang.Helper;
using HoaDonBanHang.IService;
using HoaDonBanHang.Service;
using Microsoft.AspNetCore.Mvc;


namespace HoaDonBanHang.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonService hoaDonService;
        public HoaDonController()
        {
            hoaDonService = new HoaDonService();
        }
        [HttpPost("ThemHoaDon")]
        public IActionResult ThemHoaDon(HoaDon hoaDon)
        {
            var res = hoaDonService.ThemHoaDon(hoaDon);
            return Ok(res);
        }
        [HttpPut("{HoadonId}")]
        public IActionResult SuaHoaDon(int HoadonId,HoaDon hoaDon)
        {
            var res = hoaDonService.SuaHoaDon(HoadonId,hoaDon);    
            return Ok(res);
        }
        [HttpDelete("{HoadonId}")]
        public IActionResult XoaHoaDon(int HoadonId)
        {
            hoaDonService.XoaHoaDon(HoadonId);
            return Ok();
        }
        [HttpGet]
        public IActionResult LayHoaDon( [FromQuery] string? keyword, int? year = null,
                                        [FromQuery] int? thang = null,
                                        [FromQuery] DateTime? tungay = null,
                                        [FromQuery] DateTime? denngay = null,
                                        [FromQuery] int? giatu = null,
                                        [FromQuery] int? dengia = null,
                                        [FromQuery] Pagination pagination = null)
                              
        {
            var query = hoaDonService.LayHoaDon(keyword,year,thang,tungay,denngay,giatu,dengia);
            var hoaDOn = PageResult<HoaDon>.TopPageResult(pagination, query).AsQueryable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<HoaDon>(pagination,hoaDOn);
            return Ok(res);
        }
    }
}
