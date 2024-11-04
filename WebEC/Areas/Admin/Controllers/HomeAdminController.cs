using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEC.Models;
using X.PagedList;
using WebEC.Models.Authentication;

namespace WebEC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QlbanVaLiContext Db = new QlbanVaLiContext();
        [Route("")]
        [Route("index")]
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? _page)
        {
            int pageSize = 12;
            int pageNumber = (_page == null || _page < 0) ? 1 : _page.Value;
            IOrderedQueryable<TDanhMucSp> listSanPham = Db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listSanPham, pageNumber, pageSize);
            return View(list);
        }
        [Route("themsanphammoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(
                Db.TChatLieus.ToList(),
                "MaChatLieu",
                "ChatLieu");
            ViewBag.MaHangSx = new SelectList(
                Db.THangSxes.ToList(),
                "MaHangSx",
                "HangSx");
            ViewBag.MaNuocSx = new SelectList(
                Db.TQuocGia.ToList(),
                "MaNuoc",
                "TenNuoc");
            ViewBag.MaLoai = new SelectList(
                Db.TLoaiSps.ToList(),
                "MaLoai",
                "Loai");
            ViewBag.MaDt = new SelectList(
                Db.TLoaiDts.ToList(),
                "MaDt",
                "TenLoai");
            return View();
        }
        [Route("themsanphammoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                Db.TDanhMucSps.Add(sanPham);
                Db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }
        [Route("suasanpham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(
                Db.TChatLieus.ToList(),
                "MaChatLieu",
                "ChatLieu");
            ViewBag.MaHangSx = new SelectList(
                Db.THangSxes.ToList(),
                "MaHangSx",
                "HangSx");
            ViewBag.MaNuocSx = new SelectList(
                Db.TQuocGia.ToList(),
                "MaNuoc",
                "TenNuoc");
            ViewBag.MaLoai = new SelectList(
                Db.TLoaiSps.ToList(),
                "MaLoai",
                "Loai");
            ViewBag.MaDt = new SelectList(
                Db.TLoaiDts.ToList(),
                "MaDt",
                "TenLoai");
            TDanhMucSp? sanPham = Db.TDanhMucSps.Find(maSanPham);
            return View(sanPham);
        }
        [Route("suasanpham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(sanPham).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(sanPham);
        }
        [Route("xoasanpham")]
        [HttpGet]
        public IActionResult XoaSanPham(string maSanPham)
        {
            TempData["Message"] = "";
            List<TChiTietSanPham> chiTietSanPham = 
                Db
                .TChiTietSanPhams
                .Where(x => x.MaSp == maSanPham)
                .ToList();
            if (chiTietSanPham.Count > 0)
            {
                TempData["Message"] = "Không xóa được sản phẩm";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            IQueryable<TAnhSp> anhSanPham = Db.TAnhSps.Where(x => x.MaSp == maSanPham);
            if (anhSanPham.Any())
            {
                Db.RemoveRange(anhSanPham);
            }
            Db.Remove(Db.TDanhMucSps.Find(maSanPham));
            Db.SaveChanges();
            TempData["Message"] = "Đã xóa sản phẩm";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }
    }
}
