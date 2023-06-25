using AutoMapper;
using ECommerceAPI.Data;
using ECommerceAPI.Data.Domain;
using ECommerceAPI.Schema.DataSets.Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Business.Services
{
    public class CouponService : BaseService<Coupon, CouponRequest, CouponResponse>, ICouponService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CouponService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

    }
}
