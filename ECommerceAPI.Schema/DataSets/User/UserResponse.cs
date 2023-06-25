using ECommerceAPI.Base;

namespace ECommerceAPI.Schema.DataSets.User
{
    public class UserResponse : BaseResponse
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal DigitalWallet { get; set; }
    }
}
