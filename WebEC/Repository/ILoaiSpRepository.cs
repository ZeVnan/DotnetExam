using WebEC.Models;
namespace WebEC.Repository
{
    public interface ILoaiSpRepository
    {
        TLoaiSp Add(TLoaiSp _loaiSp);
        TLoaiSp Update(TLoaiSp _loaiSp);
        TLoaiSp Delete(String _maLoaiSp);
        TLoaiSp GetLoaiSp(String _maLoaiSp);
        IEnumerable<TLoaiSp> GetAllLoaiSp();
    }
}
