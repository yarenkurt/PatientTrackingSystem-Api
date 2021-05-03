using System.Linq;
using Business.Repositories;
using Core.Results;
using Entities.Concrete;
using UnitTest.DataAccess;
using UnitTest.Entities;
using Xunit;

namespace UnitTest.Business
{
    public class ServiceRepositoryTests
    {
        [Fact]
        public async void GetById_NotNull_Dto()
        {
            //Arrange
            var dal = new MockDalRepository<Patient>().GetById(MockPatient.ToList().FirstOrDefault(x => x.Id == 1));

            //Act
            var service = new ServiceRepository<Patient>(dal.Object);
            var result = await service.GetAsync(1);
            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async void Insert_Success_DataResult()
        {
            //Arrange
            var dal = new MockDalRepository<Patient>().Insert(new DataResult<Patient>(MockPatient.ToList().FirstOrDefault(x => x.Id == 1), true));

            //Act
            var service = new ServiceRepository<Patient>(dal.Object);
            var result = await service.InsertAsync(new Patient());

            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async void Update_Success_Result()
        {
            //Arrange
            var dal = new MockDalRepository<Patient>().Update(new DataResult<Patient>(new Patient(), true), MockPatient.ToList().FirstOrDefault(x => x.Id == 1));

            //Act
            var service = new ServiceRepository<Patient>(dal.Object);
            var result = await service.UpdateAsync(new Patient());
            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async void Delete_Success_Result()
        {
            //Arrange
            var dal = new MockDalRepository<Patient>().Delete(new DataResult<Patient>(new Patient(), true), MockPatient.ToList().FirstOrDefault(x => x.Id == 1));

            //Act
            var service = new ServiceRepository<Patient>(dal.Object);
            var result = await service.DeleteAsync(1);
            //Assert
            Assert.True(result.Success);
        }
    }
}