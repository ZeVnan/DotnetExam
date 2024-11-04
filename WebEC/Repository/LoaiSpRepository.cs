using WebEC.Models;

namespace WebEC.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private QlbanVaLiContext Context { get; }
        public LoaiSpRepository(QlbanVaLiContext _context)
        {
            Context = _context;
        }
        public TLoaiSp Add(TLoaiSp _loaiSp)
        {
            Context.TLoaiSps.Add(_loaiSp);
            Context.SaveChanges();
            return _loaiSp;
        }

        public TLoaiSp Delete(string _maLoaiSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TLoaiSp> GetAllLoaiSp()
        {
            return Context.TLoaiSps;
        }

        public TLoaiSp GetLoaiSp(string _maLoaiSp)
        {
            return Context.TLoaiSps.Find(_maLoaiSp)!;
        }

        public TLoaiSp Update(TLoaiSp _loaiSp)
        {
            Context.Update(_loaiSp);
            Context.SaveChanges();
            return _loaiSp;
        }
    }
}
