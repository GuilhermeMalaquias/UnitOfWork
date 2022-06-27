using EntityFrameworkAbstraction.Model;

namespace EntityFrameworkAbstraction.Interfaces;

public interface IUnitOfWork
{
    IRepository<User> UserRepository { get; }
    void Commit();
    void Rollback();
}