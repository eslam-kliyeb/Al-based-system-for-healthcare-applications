using AI.Core.Entities;
using AI.Core.DTOs;
using AutoMapper;
using AI.Repository.Resolver;

namespace AI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Patient, PatientToReturnDto>()
                     .ForMember(d => d.MriUrl, o => o.MapFrom<PictureUrlResolverMriUrl>())
                     .ForMember(d=> d.segmentUrl, o=> o.MapFrom<PictureUrlResolverSegmentUrl>());
            CreateMap<PatientToReturnDto,Patient> ();
            CreateMap<PatientInputDto, Patient>();
            CreateMap<DoctorDto, Doctor>().ReverseMap();
        }
    }
}
