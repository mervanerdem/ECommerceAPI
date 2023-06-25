using ECommerceAPI.Base;
using ECommerceAPI.Data.Repository;
using System;
using System.Linq;
using System.Transactions;

namespace ECommerceAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext dbContext;
        private bool disposed;
        public IGenericRepository<User> UserRepository { get; private set; }
        public UnitOfWork(EFContext dbContext)
        {
            this.dbContext = dbContext;
            UserRepository = new GenericRepository<User>(dbContext);

        }

        public IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel
        {
            return new GenericRepository<Entity>(dbContext);
        }


        public int Complete()
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    var result = dbContext.SaveChanges();
                    transaction.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database işleminde bir hata oluştu.");
            }

        }

        public void CompleteWithTransaction()
        {
            using (var dbDcontextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.SaveChanges();
                    dbDcontextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    // logging
                    dbDcontextTransaction.Rollback();
                }
            }
        }

        public void Dispose()
        {
            Clean(true);
        }

        private void Clean(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && dbContext is not null)
                {
                    dbContext.Dispose();
                }
            }

            disposed = true;
            GC.SuppressFinalize(this);
        }

    }
}
