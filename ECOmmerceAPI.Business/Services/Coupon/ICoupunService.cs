using ECommerceAPI.Data.Domain;
using ECommerceAPI.Schema.DataSets.Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Business.Services
{
    public interface ICouponService : IBaseService<Coupon, CouponRequest, CouponResponse>
    {


    }
}
