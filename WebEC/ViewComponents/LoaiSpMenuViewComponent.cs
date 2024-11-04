using Microsoft.AspNetCore.Mvc;
using WebEC.Models;
using WebEC.Repository;

namespace WebEC.ViewComponents
{
    public class LoaiSpMenuViewComponent : ViewComponent
    {
        private ILoaiSpRepository LoaiSp { get;}
        public LoaiSpMenuViewComponent (ILoaiSpRepository _loaiSpRepository)
        {
            this.LoaiSp = _loaiSpRepository;
        }
        public IViewComponentResult Invoke()
        {
            IEnumerable<TLoaiSp> loaiSp = this.LoaiSp.GetAllLoaiSp().OrderBy(x => x.Loai);
            return View(loaiSp);
        }
    }
}
