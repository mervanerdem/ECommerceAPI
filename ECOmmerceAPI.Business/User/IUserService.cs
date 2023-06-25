using ECommerceAPI.Base;
using ECommerceAPI.Data;
using ECommerceAPI.Schema.DataSets.Admin;
using ECommerceAPI.Schema.DataSets.User;

namespace ECommerceAPI.Business.Users
{
    public interface IUserService : IBaseService<User, UserRequest, UserResponse>
    {
        public ApiResponse<UserResponse> UserBalance(int userId);
        public ApiResponse UpdateUserByAdmin(int Id, AdminRequest request);
        public ApiResponse<List<AdminResponse>> GetAllUser();
    }
}
