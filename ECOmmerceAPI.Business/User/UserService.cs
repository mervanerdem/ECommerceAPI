using AutoMapper;
using ECommerceAPI.Base;
using ECommerceAPI.Base.Helper;
using ECommerceAPI.Data;
using ECommerceAPI.Schema.DataSets.Admin;
using ECommerceAPI.Schema.DataSets.User;
using ECommerceAPI.Schema.FluentValidation;

namespace ECommerceAPI.Business.Users
{
    public class UserService : BaseService<User, UserRequest, UserResponse>, IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public ApiResponse<List<AdminResponse>> GetAllUser()
        {
            var users = unitOfWork.Repository<User>().GetAll().ToList();
            var userResponse = mapper.Map<List<AdminResponse>>(users);
            return new ApiResponse<List<AdminResponse>>(userResponse);
        }

        public ApiResponse Insert(UserRequest request)
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                string errorMessage = string.Join(Environment.NewLine, validationResult.Errors.Select(error => error.ErrorMessage));
                return new ApiResponse(errorMessage);
            }
            var exist = unitOfWork.Repository<User>().
            Where(x => x.Email.Equals(request.Email) || x.UserName.Equals(request.UserName)).ToList();

            if (exist.Any())
            {
                return new ApiResponse("Email or Username already in use.");
            }

            try
            {
                request.Password = CreateMD5(request.Password);
                var entity = mapper.Map<UserRequest, User>(request);
                entity.CreatedAt = DateTime.UtcNow;
                entity.Status = 1;
                entity.UpdatedBy = "Admin";

                unitOfWork.Repository<User>().Insert(entity);
                unitOfWork.Complete();
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public ApiResponse UpdateUser(int Id, UserRequest request)
        {
            var exist = unitOfWork.Repository<User>().GetByIdAsNoTracking(Id);
            if (exist is null)
            {
                return new ApiResponse("User not found.");
            }

            //unitOfWork.Dispose();
            return Update(Id, request);
        }

        public ApiResponse UpdateUserByAdmin(int Id, AdminRequest request)
        {
            var user = unitOfWork.Repository<User>().Where(x => x.Id.Equals(Id)).FirstOrDefault();
            if (user == null)
            {
                return new ApiResponse("Record Not Found.");
            }
            var existingUser = unitOfWork.Repository<User>().GetById(Id);
            // Role alanını korumak için geçici bir değişken tanımla
            var tempRole = existingUser.Role;
            request.Password = JwtHelper.CreateMD5(request.Password);
            // request nesnesini existingUser nesnesine eşle
            mapper.Map(request, existingUser);
            // Role alanını geçici değişkenle güncelle
            existingUser.Role = tempRole;
            existingUser.Id = Id;
            existingUser.UpdatedAt = DateTime.UtcNow;

            unitOfWork.Repository<User>().Update(existingUser);

            if (unitOfWork.Complete() > 0)
            {
                return new ApiResponse();
            }
            return new ApiResponse("Internal Server Error");
        }

        public ApiResponse<UserResponse> UserBalance(int userId)
        {
            var user = unitOfWork.Repository<User>().GetById(userId);
            var userResponse = mapper.Map<UserResponse>(user);
            return new ApiResponse<UserResponse>(userResponse);
        }

        private string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();

            }
        }

    }
}
