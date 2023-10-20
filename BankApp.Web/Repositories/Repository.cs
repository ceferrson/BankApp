using Microsoft.EntityFrameworkCore.Diagnostics;
using BankApp.Web.Data.Contexts;
using BankApp.Web.Interfaces;

namespace BankApp.Web.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly BankContext _context;

        public Repository(BankContext context) 
        {
            _context = context;
        }
        public bool Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return true;
        }

        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return true;
        }

        public bool Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return true;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
