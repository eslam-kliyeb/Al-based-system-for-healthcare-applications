using AI.Core.Entities;
using AI.Core.DTOs;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace AI.Repository.Resolver
{
    public class PictureUrlResolverMriUrl : IValueResolver<Patient, PatientToReturnDto, string>
    {
        private readonly IConfiguration _configuration;
        public PictureUrlResolverMriUrl(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Patient source, PatientToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.MriUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.MriUrl}";
            }
            return string.Empty;
        }
    }
}
