using ECommerceAPI.Base;
using ECommerceAPI.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Data
{
    public interface IUnitOfWork :IDisposable
    {
        IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel;

        void Complete();
        void CompleteWithTransaction();
    }
}
