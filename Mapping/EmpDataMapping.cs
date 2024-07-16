using AutoMapper;
using dotnet60_example.Entities;
using dotnet60_example.ViewModels;

namespace dotnet60_example.Mapping
{
    public class EmpDataMapping : Profile
    {
        public EmpDataMapping()
        {
            CreateMap<EmpData, EmpDataVM>()
                .ForMember(des => des.UserId, opt => opt.MapFrom(x => x.Id))
                .ForMember(des => des.UserName, opt => opt.MapFrom(x => x.Name));

            CreateMap<EmpData, Account>()
                .ForMember(des => des.UserId, opt => opt.MapFrom(x => x.Id))
                .ForMember(des => des.UserName, opt => opt.MapFrom(x => x.Name));
        }
    }
}
