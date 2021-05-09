using Business.Abstract;
using Business.Concrete;
using Core.Token;
using DataAccess.Repositories;
using DataAccess.Repositories.EF;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions
{
    public static class InstallerExtension
    {
        public static void InstallService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddTransient<SmsHelper>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IDistrictService, DistrictService>();
            services.AddTransient<IHospitalService, HospitalService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IAdviceService, AdviceService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IDegreeService, DegreeService>();
            services.AddTransient<IDiseaseService, DiseaseService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IPatientDiseaseService, PatientDiseaseService>();
            services.AddTransient<IDoctorPatientService, DoctorPatientService>();
            services.AddTransient<IPatientRelativeService, PatientRelativeService>();
            services.AddTransient<IPersonLoginHistoryService, PersonLoginHistoryService>();
            services.AddTransient<IQuestionPoolService,QuestionPoolService>();
            services.AddTransient<IUserService,UserService>();

        }
    }
}