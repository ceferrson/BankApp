namespace BankApp.Web.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        bool Create(T entity);
        bool Update(T entity);
        bool Remove(T entity);
        bool Save();
        List<T> GetAll();
        T GetById(int id);
        IQueryable<T> GetQueryable();
    }
}
