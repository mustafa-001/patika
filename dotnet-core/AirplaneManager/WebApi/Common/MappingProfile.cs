using AutoMapper;
using WebApi.Entities;
using WebApi.FligthOperations;
using WebApi.PilotOperations;

namespace WebApi.Common
{
    

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        // CreateMap<Pilot, int>().ForMember((d => d), (opt => opt.MapFrom(s => s.Id)));
        CreateMap<Fligth, FligthViewModel>()
        .IncludeAllDerived()
        // .Include<Fligth, FligthDetailsViewModel>()
        .ForMember((x=> x.Company), (opt => opt.MapFrom(src=> src.Company.Name)))
            .ForMember((dest => dest.ArrivalAirfield), (opt => opt.MapFrom(src=> src.ArrivalAirfield.Id)))
            .ForMember((dest => dest.DepartureAirfield), (opt => opt.MapFrom(src=> src.DepartureAirfield.Id))) .ForMember((d => d.PilotIds), (opt => opt.MapFrom(s => s.Pilots.Select(x => x.Id).ToList())));
        
        CreateMap<Fligth, FligthDetailsViewModel>();

        CreateMap<FligthViewModel, Fligth>()
        .ForMember((dest => dest.Company), (opt => opt.MapFrom(src => new Company{Name = src.Company})))
        .ForMember((d=> d.ArrivalAirfield), (opt => opt.MapFrom(src => new Airfield{Id = src.ArrivalAirfield})))
        .ForMember((d => d.Plane), (opt => opt.MapFrom(src => new Plane{Id = src.PlaneId})))
        .ForMember((d=> d.DepartureAirfield), (opt => opt.MapFrom(src => new Airfield{Id = src.DepartureAirfield})))
        .ForMember((x=> x.Pilots), (opt => opt.MapFrom(src=> src.PilotIds.Select(x=> new Pilot{Id = x}).ToList())));

    }
}
}