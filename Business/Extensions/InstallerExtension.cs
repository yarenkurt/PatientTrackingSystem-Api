using Business.Abstract;
using Business.Concrete;
using Core.Models;
using Core.Sms;
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
            services.AddSingleton(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddSingleton<SmsHelper>();
            services.AddSingleton<ICountryService, CountryService>();
            services.AddSingleton<ICityService, CityService>();
            services.AddSingleton<IDistrictService, DistrictService>();
            services.AddSingleton<IHospitalService, HospitalService>();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IAdminService, AdminService>();
            services.AddSingleton<IAdviceService, AdviceService>();
            services.AddSingleton<IAppointmentService, AppointmentService>();
            services.AddSingleton<IDoctorService, DoctorService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDegreeService, DegreeService>();
            services.AddSingleton<IDiseaseService, DiseaseService>();
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IPatientDiseaseService, PatientDiseaseService>();
            services.AddSingleton<IDoctorPatientService, DoctorPatientService>();
            services.AddSingleton<IPatientRelativeService, PatientRelativeService>();
            services.AddSingleton<IPersonLoginHistoryService, PersonLoginHistoryService>();
            services.AddSingleton<IQuestionPoolService,QuestionPoolService>();
            services.AddSingleton<IUserService,UserService>();

        }
    }
}