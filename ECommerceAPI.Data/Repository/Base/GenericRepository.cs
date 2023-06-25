using ECommerceAPI.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceAPI.Data.Repository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
    {
        protected readonly EFContext dbContext;
        private bool disposed;
        private readonly DbSet<Entity> DbSet;

        public GenericRepository(EFContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<Entity>();
        }

        public void Delete(Entity entity)
        {
            dbContext.Set<Entity>().Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = dbContext.Set<Entity>().Find(id);
            dbContext.Set<Entity>().Remove(entity);
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

        public IQueryable<Entity> GetAll()
        {
            return dbContext.Set<Entity>();
        }

        public List<Entity> GetAllAsNoTracking()
        {
            return dbContext.Set<Entity>().AsNoTracking().ToList();
        }

        public IQueryable<Entity> GetAsQueryable()
        {
            return dbContext.Set<Entity>().AsQueryable();
        }

        public Entity GetById(int id)
        {
            return dbContext.Set<Entity>().Find(id);
        }

        public Entity GetByIdAsNoTracking(int id)
        {
            return dbContext.Set<Entity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Entity entity)
        {
            dbContext.Set<Entity>().Add(entity);
        }

        public void Update(Entity entity)
        {
            dbContext.Set<Entity>().Update(entity);

        }

        public IQueryable<Entity> Where(Expression<Func<Entity, bool>> expression)
        {
            return dbContext.Set<Entity>().Where(expression).AsQueryable();
        }

        public IEnumerable<Entity> WhereAsNoTracking(Expression<Func<Entity, bool>> expression)
        {
            return dbContext.Set<Entity>().AsNoTracking().Where(expression).AsQueryable();
        }

        private void Clean(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }

            disposed = true;
            GC.SuppressFinalize(this);
        }
        public void Dispose()
        {
            Clean(true);
        }

        public void Add(Entity entity)
        {
            DbSet.Add(entity);
        }
    }
}
