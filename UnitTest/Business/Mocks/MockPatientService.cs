using System.Collections.Generic;
using Business.Abstract;
using Core.Results;
using Entities.Concrete;
using Entities.Dtos;
using Moq;

namespace UnitTest.Business.Mocks
{
    public class MockPatientService : Mock<IPatientService>
    {
        public MockPatientService GetAll(List<GetPatientDto> result)
        {
            Setup(x => x.GetAllAsync()).ReturnsAsync(result);
            return this;
        }


        public MockPatientService Get(GetPatientDto result)
        {
            Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(result);
            return this;
        }

        public MockPatientService Insert(DataResult<GetPatientDto> result)
        {
            Setup(x => x.InsertAsync(It.IsAny<InsertPatientDto>())).ReturnsAsync(result);
            return this;
        }

        public MockPatientService Update(Result result, Patient entityResult)
        {
            Setup(x => x.UpdateAsync(It.IsAny<Patient>())).ReturnsAsync(result);
            return this;
        }

        public MockPatientService Delete(Result result, Result entityResult)
        {
            Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(result);
            return this;
        }
    }
}