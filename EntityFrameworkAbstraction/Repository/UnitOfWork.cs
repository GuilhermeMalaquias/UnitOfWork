using EntityFrameworkAbstraction.Context;
using EntityFrameworkAbstraction.Interfaces;
using EntityFrameworkAbstraction.Model;

namespace EntityFrameworkAbstraction.Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    
    private readonly DefaultDbContext _context;
    private Repository<User>? _userRepository;

    public UnitOfWork(DefaultDbContext context)
    {
        _context = context;
    }

    public IRepository<User> UserRepository => _userRepository ??= new Repository<User>(_context);

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Rollback()
    {
        Dispose();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
    
}