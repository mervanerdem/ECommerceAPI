using System;
using System.Linq;

namespace ECommerceAPI.Data.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUsername(string userName);
    }
}
