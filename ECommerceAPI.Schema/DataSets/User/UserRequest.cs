﻿using ECommerceAPI.Base;

namespace ECommerceAPI.Schema.DataSets.User
{
    public class UserRequest : BaseRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
