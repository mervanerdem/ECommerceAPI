using ECommerceAPI.Base;
using ECommerceAPI.Data.Repository;
using System;
using System.Linq;

namespace ECommerceAPI.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel;
        int Complete();
        void CompleteWithTransaction();
    }
}
