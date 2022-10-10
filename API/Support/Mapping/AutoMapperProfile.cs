using API.Contracts;
using API.Contracts.Client;
using AutoMapper;

namespace API.Support.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Request, Guest>(MemberList.None)
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName));
        }
    }
}
