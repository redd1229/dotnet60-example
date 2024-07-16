using AutoMapper;
using dotnet60_example.Entities;
using dotnet60_example.ViewModels;

namespace dotnet60_example.Mapping
{
    public class AccountMapping : Profile
    {
        public AccountMapping()
        {
            CreateMap<AccountVM, Account>();

            CreateMap<Account, AccountVM>();

            CreateMap<AccountSearchVM, Account>();

            CreateMap<Account, AccountSearchVM>();

            CreateMap<AccountEditVM, Account>();

            CreateMap<Account, AccountEditVM>();

            CreateMap<AccountCreateVM, Account>();

            CreateMap<Account, AccountCreateVM>();
        }
    }
}