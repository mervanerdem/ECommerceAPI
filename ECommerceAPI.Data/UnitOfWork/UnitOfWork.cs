using ECommerceAPI.Base;
using ECommerceAPI.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Data
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly EFContext dbContext;
        private bool disposed;
        public UnitOfWork(EFContext dbContext)
        {
            this.dbContext = dbContext;

            //CategoryRepository = new GenericRepository<Category>(dbContext);
            //DapperAccountRepository = new DapperAccountRepository(dapperDbContext);
            //DapperTransactionRepository = new DapperTransactionRepository(dapperDbContext);
        }
        public void Complete()
        {
            dbContext.SaveChanges();
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

        public IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel
        {
            return new GenericRepository<Entity>(dbContext);
        }
    }
}
