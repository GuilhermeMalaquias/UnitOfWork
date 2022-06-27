using System.Linq.Expressions;
using EntityFrameworkAbstraction.Model;

namespace EntityFrameworkAbstraction.Interfaces;

public interface IRepository<T> where T : Entity
{
    public Task<IEnumerable<T>> ListNoTrackingAsync();
    public Task<IEnumerable<T>> ListNoTrackingAsync(string query);
    public Task<IEnumerable<T>> ListNoTrackingAsync(string query, params object[] parameters);
    public Task<IEnumerable<T>> ListWithTrackingAsync();
    public Task<IEnumerable<T>> ListWithTrackingAsync(string query);
    public Task<IEnumerable<T>> ListWithTrackingAsync(string query, params object[] parameters);
    public Task CreateAsync(T entity);
    public void Create(string query, params object[] parameters);
    public void Update(T entity);
    public void Update(string query, params object[] parameters);
    public void Delete(Guid id);
    public void Delete(string query, params object[] parameters);
    public Task<T> GetByIdAsync(Guid id);
    public Task<T> GetByIdAsync(int id);
    public Task<T> GetByIdAsync(string id);
    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    public IQueryable<T> GetQueryable();
    
}