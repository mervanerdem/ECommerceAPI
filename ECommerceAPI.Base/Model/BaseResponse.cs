using System;
using System.Linq;

namespace ECommerceAPI.Base
{
    public abstract class BaseResponse
    {
        //responcelar burdan türeyecek. Bu şekilde hepsini tek merkezden yönetmek daha kolay olur.
        public int Id { get; set; }
    }
}
