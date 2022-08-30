using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCAirLine.Controllers;
using MVCAirLine.Models;
using MVCAirLine.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace AirLine_TestCases
{


    public class AirLineTestData
    {
        public void Index_ReturnsAViewResult_WithAListOfAirLines()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<AirViewModel>>();

            mockRepo.Setup(repo => repo.GetAll()).Returns(AirLineTestData.GetTestAirLine());

            var controller = new AirController(mockRepo.Object);

            // Act
            var result = controller.Index();

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<List<AirViewModel>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());

        }

        private static IEnumerable<AirViewModel> GetTestAirLine()
        {
            throw new NotImplementedException();
        }



    }
}

