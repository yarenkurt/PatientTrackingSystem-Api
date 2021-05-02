using System.Collections.Generic;
using System.Linq;
using Entities.Concrete;

namespace UnitTest.Entities
{
    public class MockPatient : List<Patient>
    {
        private MockPatient(IEnumerable<Patient> Patients)
        {
            AddRange(Patients);
        }

        public new static int Count()
        {
            return ToList().Count;
        }

        public static List<Patient> ToList()
        {
            return new()
            {
                new Patient
                {
                    Id = 1, Email = "yarenkurt078@outlook.com", IdentityNumber = "12345678901", PersonId = 1
                },
                new Patient
                {
                    Id = 2, Email = "kubrakesici@outlook.com", IdentityNumber = "12345678902", PersonId = 2
                },
                new Patient
                {
                    Id = 2, Email = "kardelensimsek@outlook.com", IdentityNumber = "12345678903", PersonId = 3
                }
            };
        }

        public static IQueryable<Patient> AsQueryable()
        {
            return ToList().AsQueryable();
        }

    }
}