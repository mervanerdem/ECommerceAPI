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
        Entity GetByIdWithInclude(int id, params string[] includes);
        void Insert(Entity entity);
        void Update(Entity entity);
        void DeleteById(int id);
        void Delete(Entity entity);
        List<Entity> GetAll();
        IQueryable<Entity> GetAsQueryable();
        List<Entity> GetAllAsNoTracking();
        List<Entity> GetAllWithInclude(params string[] includes);
        IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression);
        IEnumerable<Entity> WhereAsNoTracking(Expression<Func<Entity, bool>> expression);
        IEnumerable<Entity> WhereWithInclude(Expression<Func<Entity, bool>> expression, params string[] includes);

        void Add(Entity entity);
        void Complete();
        void CompleteWithTransaction();
    }
}
