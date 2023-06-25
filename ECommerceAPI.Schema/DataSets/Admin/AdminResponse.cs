using ECommerceAPI.Base;

namespace ECommerceAPI.Schema.DataSets.Admin
{
    public class AdminResponse : BaseResponse
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public decimal DigitalWallet { get; set; }
    }
}
