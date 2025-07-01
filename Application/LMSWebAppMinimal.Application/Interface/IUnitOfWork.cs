using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Model;

namespace LMSWebAppMinimal.Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<BaseUser> Users { get; }
        
        Task<int> SaveChangesAsync();
        int SaveChanges();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}