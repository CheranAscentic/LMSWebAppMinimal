using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Data.Context;
using LMSWebAppMinimal.Data.Repository;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Model;
using Microsoft.EntityFrameworkCore.Storage;

namespace LMSWebAppMinimal.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataDBContext context;
        private IDbContextTransaction? transaction;
        
        private IRepository<Book>? books;
        private IRepository<BaseUser>? users;

        public UnitOfWork(DataDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<Book> Books =>
            books ??= new DatabaseRepository<Book>(context);

        public IRepository<BaseUser> Users =>
            users ??= new DatabaseRepository<BaseUser>(context);

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public async Task BeginTransactionAsync()
        {
            transaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (transaction != null)
            {
                await transaction.CommitAsync();
                await transaction.DisposeAsync();
                transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
                transaction = null;
            }
        }

        public void Dispose()
        {
            transaction?.Dispose();
            context.Dispose();
        }
    }
}