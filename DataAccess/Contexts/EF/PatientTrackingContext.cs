using DataAccess.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts.EF
{
    public class PatientTrackingContext : DbContext
    {
        public PatientTrackingContext()
        {
        }

        public PatientTrackingContext(DbContextOptions<PatientTrackingContext> options) : base(options)
        {
            
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments  { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<DoctorAdvice> DoctorAdvices { get; set; }
        public DbSet<PasswordChangeRequest> PasswordChangeRequests { get; set; }
        public DbSet<PersonLoginHistory> PersonLoginHistories { get; set; }
        public DbSet<QuestionPool> QuestionPools { get; set; }
        public DbSet<PatientAnswer> PatientAnswers { get; set; }
        public DbSet<PatientDisease> PatientDiseases { get; set; }
        public DbSet<PatientRelative> PatientRelatives { get; set; }
        public DbSet<AnswerPool> AnswerPools { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseSqlServer("Server=217.116.200.103,2000; Database=PatientTrackingDB; User=sa; Password = Yaren#1998;");
        }

        
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb = mb.SetDataType();
            mb = mb.MapConfiguration();
            base.OnModelCreating(mb);
        }
    }
}