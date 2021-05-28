

using System.Collections.Generic;
using Api.Controllers;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using UnitTest.Business.Mocks;
using Xunit;

namespace UnitTest.Api.Controller
{
    public class PatientControllerTests
    {
        [Fact]
        public async void GetAll_NotNull_OkAndPagedList()
        {
            var service = new MockPatientService().GetAll(new List<GetPatientDto>());
            var controller = new PatientsController(service.Object,null);

            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

       

    }
}