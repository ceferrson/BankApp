using BankApp.Web.Data.Contexts;
using BankApp.Web.Interfaces;
using BankApp.Web.Repositories;

namespace BankApp.Web.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly BankContext _context;

        public Uow(BankContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
