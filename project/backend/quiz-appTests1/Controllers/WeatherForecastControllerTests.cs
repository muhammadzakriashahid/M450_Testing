using Microsoft.VisualStudio.TestTools.UnitTesting;
using quiz_app.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace quiz_app.Controllers.Tests
{
    [TestClass]
    public class WeatherForecastControllerTests
    {
        private WeatherForecastController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Arrange: Initialize the controller before each test
            _controller = new WeatherForecastController();
        }

        [TestMethod]
        public void WeatherForecastControllerTest()
        {
            // Act: Create an instance of the controller
            var controller = new WeatherForecastController();

            // Assert: Verify the controller is not null
            Assert.IsNotNull(controller, "Controller instance should not be null.");
        }

        [TestMethod]
        public void GetTest()
        {
            // Act: Call the Get method
            var result = _controller.Get();

            // Assert: Verify the result is not null and contains data
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsInstanceOfType(result, typeof(IEnumerable<WeatherForecast>), "Result should be of type IEnumerable<WeatherForecast>.");
            Assert.IsTrue(((IEnumerable<WeatherForecast>)result).Any(), "Result should contain at least one WeatherForecast.");
        }
    }
}