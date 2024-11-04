using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebEC.Models;
using X.PagedList;
using WebEC.Models.Authentication;

namespace WebEC.Controllers
{
	
	public class HomeController : Controller
	{
		QlbanVaLiContext Db = new QlbanVaLiContext();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		[Authentication]
		public IActionResult Index(int? _page)
		{
			int pageSize = 8;
			int pageNumber = (_page == null || _page < 0) ? 1 : _page.Value;
			IOrderedQueryable<TDanhMucSp> listSanPham = Db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
			PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listSanPham, pageNumber, pageSize);
			return View(list);
		}
		public IActionResult SanPhamTheoLoai(String _maLoai, int? _page)
		{
            int pageSize = 8;
            int pageNumber = (_page == null || _page < 0) ? 1 : _page.Value;
            IOrderedQueryable<TDanhMucSp> listSanPham = Db
				.TDanhMucSps
				.AsNoTracking()
				.Where(x=>x.MaLoai == _maLoai)
				.OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listSanPham, pageNumber, pageSize);
			ViewBag.maLoai = _maLoai;
            return View(list);
		}
		public IActionResult ChiTietSanPham(string _maSp)
		{
			TDanhMucSp? sanPham =  Db.TDanhMucSps.SingleOrDefault(x => x.MaSp == _maSp);
			List<TAnhSp> anhSanPham = Db.TAnhSps.Where(x => x.MaSp == _maSp).ToList();
			ViewBag.anhSanPham = anhSanPham;
			return View(sanPham);
		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
