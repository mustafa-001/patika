

using AutoMapper;
using WebApi.Entities;
using WebApi.FligthOperations;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Fligth, FligthViewModel>().ForMember((x=> x.Company), (opt => opt.MapFrom(src=> src.Company.Name)))
        .ForMember((dest => dest.PilotIds), (opt=> opt.MapFrom(src => src.Pilots.Select(p => p.Id).ToList())));
    }
}
