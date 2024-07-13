using AI.Core.Interfaces.Repositories;
using AI.Core.Interfaces.Service;
using AI.Helper;
using AI.Repository.Repositories;
using AI.Repository.Resolver;
using AI.Services;

namespace AI.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IChatBotService, ChatBotService>();
            services.AddScoped<IClassifierService, ClassifierService>();
            services.AddScoped<ISegmentationService, SegmentationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICashService, CashService>();
            services.AddScoped<PictureUrlResolverMriUrl>();
            services.AddScoped<PictureUrlResolverSegmentUrl>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            return services;
        }
    }
}
