using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Base
{
    public abstract class BaseResponse
    {
        //responcelar burdan türeyecek. Bu şekilde hepsini tek merkezden yönetmek daha kolay olur.
        public int Id { get; set; }
    }
}
