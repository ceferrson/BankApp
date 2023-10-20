using AutoMapper;
using BankApp.Web.Data.Entities;
using BankApp.Web.Models.Dto;

namespace BankApp.Web.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
        }
    }
}
