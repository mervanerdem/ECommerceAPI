using Microsoft.AspNetCore.Http;
using System.Text;

namespace ECommerceAPI.Base.Helper
{
    public static class JwtHelper
    {
        //şifreleme yapar
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();

            }
        }


        public static int GetUserIdFromContext(HttpContext context)
        {
            var userId = int.Parse(context.Items["UserId"] as string);
            return userId;
        }

        //Random sipariş numarası oluşturma
        public static string GenerateOrderNumber()
        {
            //Sipariş Numarasının oluşacağı krakterler (basit olsun diye sadece sayılar)
            string charset = "0123456789";

            int length = 8;
            Random random = new Random();

            StringBuilder orderNumberBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(0, charset.Length);
                char randomChar = charset[randomIndex];
                orderNumberBuilder.Append(randomChar);
            }

            string orderNumber = orderNumberBuilder.ToString();
            return orderNumber;
        }


    }
}
