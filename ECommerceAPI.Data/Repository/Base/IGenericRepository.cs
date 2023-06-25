using ECommerceAPI.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerceAPI.Data.Repository
{
    public interface IGenericRepository<Entity> where Entity : BaseModel
    {
        Entity GetById(int id);
        Entity GetByIdAsNoTracking(int id);
        void Insert(Entity entity);
        void Update(Entity entity);
        void DeleteById(int id);
        void Delete(Entity entity);
        IQueryable<Entity> GetAll();
        IQueryable<Entity> GetAsQueryable();
        List<Entity> GetAllAsNoTracking();
        IQueryable<Entity> Where(Expression<Func<Entity, bool>> expression);
        IEnumerable<Entity> WhereAsNoTracking(Expression<Func<Entity, bool>> expression);

        void Add(Entity entity);
        void Complete();
        void CompleteWithTransaction();
    }
}
