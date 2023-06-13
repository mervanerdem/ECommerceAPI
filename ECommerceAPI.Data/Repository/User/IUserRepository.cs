using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Data.Repository
{
    public interface IUserRepository :IGenericRepository<User>
    {
        User GetUsername(string userName);
    }
}
