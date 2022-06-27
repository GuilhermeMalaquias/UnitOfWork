using System.Linq.Expressions;
using EntityFrameworkAbstraction.Interfaces;
using EntityFrameworkAbstraction.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkAbstraction.Repository;

public class Repository<T> : IRepository<T> where T : Entity, new()
{
    private readonly DbSet<T> _dbSet;
    public Repository(DbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> ListNoTrackingAsync()
    {
        return await _dbSet.Where(del => del.D_E_L_E_T_ != true)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> ListNoTrackingAsync(string query)
    {
        return await _dbSet.FromSqlRaw(query).AsNoTrackingWithIdentityResolution().ToListAsync();
    }

    public async Task<IEnumerable<T>> ListNoTrackingAsync(string query, params object[] parameters)
    {
       return parameters == null ? throw new Exception() : 
           await _dbSet.FromSqlRaw(query, parameters).AsNoTrackingWithIdentityResolution().ToListAsync(); 
        
    }

    public async Task<IEnumerable<T>> ListWithTrackingAsync()
    {
        return await _dbSet.Where(del => del.D_E_L_E_T_ != true)
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> ListWithTrackingAsync(string query)
    {
        return await _dbSet.FromSqlRaw(query).ToListAsync();
    }

    public async Task<IEnumerable<T>> ListWithTrackingAsync(string query, params object[] parameters)
    {
        return parameters == null ? throw new Exception() : 
            await _dbSet.FromSqlRaw(query, parameters).ToListAsync(); 
    }

    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Create(string query, params object[] parameters)
    {
        _dbSet.FromSqlRaw(query, parameters);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Update(string query, params object[] parameters)
    {
        _dbSet.FromSqlRaw(query, parameters);
    }

    public void Delete(Guid id)
    {
         var entity =  GetByIdAsync(id).Result;
         entity.D_E_L_E_T_ = true;
        _dbSet.Update(entity);
    }

    public void Delete(string query, params object[] parameters)
    {
        _dbSet.FromSqlRaw(query, parameters);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) ?? new T();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id) ?? new T();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id) ?? new T();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public IQueryable<T> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }
}