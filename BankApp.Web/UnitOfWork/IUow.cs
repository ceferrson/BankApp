using BankApp.Web.Interfaces;

namespace BankApp.Web.UnitOfWork
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : class, new();
        void Save();
    }
}
