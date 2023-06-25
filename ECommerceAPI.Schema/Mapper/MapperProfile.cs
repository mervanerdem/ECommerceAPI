using AutoMapper;
using ECommerceAPI.Data;
using ECommerceAPI.Schema.DataSets.Admin;
using ECommerceAPI.Schema.DataSets.User;
using System.Security.Principal;

namespace ECommerceAPI.Schema
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserRequest, User>();
            CreateMap<AdminRequest, User>();
            CreateMap<User, AdminResponse>();
        }
    }
}
