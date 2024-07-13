using AI.Core.Entities;
using AI.Core.DTOs;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace AI.Repository.Resolver
{
    public class PictureUrlResolverSegmentUrl : IValueResolver<Patient, PatientToReturnDto, string>
    {
        private readonly IConfiguration _configuration;
        public PictureUrlResolverSegmentUrl(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Patient source, PatientToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.segmentUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.segmentUrl}";
            }
            return string.Empty;
        }
    }

}
