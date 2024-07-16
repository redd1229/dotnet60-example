using AutoMapper;
using dotnet60_example.Entities;
using dotnet60_example.ViewModels;

namespace dotnet60_example.Mapping
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleVM>();

            CreateMap<RoleVM, Role>();

            CreateMap<Role, RoleSearchVM>();

            CreateMap<RoleSearchVM, Role>();

            CreateMap<Role, RoleCreateVM>();

            CreateMap<RoleCreateVM, Role>();

            CreateMap<Role, RoleEditVM>();

            CreateMap<RoleEditVM, Role>();
        }
    }
}
