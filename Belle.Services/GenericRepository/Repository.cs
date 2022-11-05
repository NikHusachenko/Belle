using Belle.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Belle.Services.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return _table.AsNoTracking().ToList();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _table.AsNoTracking().Where(predicate).ToList();
        }
        public T FindById(int id)
        {
            return _table.Find(id);
        }

        public void Create(T item)
        {
            _table.Add(item);
            _context.SaveChanges();
        }
        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(T item)
        {
            _table.Remove(item);
            _context.SaveChanges();
        }
    }
}