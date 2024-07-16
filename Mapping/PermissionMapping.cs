using AutoMapper;
using dotnet60_example.Entities;
using dotnet60_example.ViewModels;

namespace dotnet60_example.Mapping
{
    public class PermissionMapping : Profile
    {
        public PermissionMapping()
        {
            CreateMap<Permission, PermissionVM>();

            CreateMap<PermissionVM, Permission>();

            CreateMap<Permission, PermissionSearchVM>();

            CreateMap<PermissionSearchVM, Permission>();

            CreateMap<Permission, PermissionCreateVM>();

            CreateMap<PermissionCreateVM, Permission>();

            CreateMap<Permission, PermissionEditVM>();

            CreateMap<PermissionEditVM, Permission>();
        }
    }
}
