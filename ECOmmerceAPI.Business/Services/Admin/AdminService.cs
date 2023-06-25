using AutoMapper;
using ECommerceAPI.Base;
using ECommerceAPI.Base.Helper;
using ECommerceAPI.Data;
using ECommerceAPI.Schema.DataSets.User;

namespace ECommerceAPI.Business.Services.Admin
{
    public class AdminService : BaseService<User, UserRequest, UserResponse>, IAdminService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AdminService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // Admin kayıt işlemi yapılır. Sadece admin diğer admini kaydedebilir.
        public override ApiResponse Insert(UserRequest request)
        {
            var exist = unitOfWork.Repository<User>().
            Where(x => x.Email.Equals(request.Email)).FirstOrDefault();

            if (exist != null)
            {
                return new ApiResponse("Email already in use.");
            }

            try
            {
                request.Password = JwtHelper.CreateMD5(request.Password);
                var entity = mapper.Map<UserRequest, User>(request);
                entity.CreatedAt = DateTime.UtcNow;
                entity.Status = 1;
                entity.Role = "Admin";
                
                unitOfWork.Repository<User>().Add(entity);
                if (unitOfWork.Complete() > 0)
                {
                    return new ApiResponse();
                }
                return new ApiResponse("Internal Server Error");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }


        }
    }
}
