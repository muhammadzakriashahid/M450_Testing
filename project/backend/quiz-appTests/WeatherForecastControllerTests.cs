using Microsoft.Extensions.Logging;
using Moq;
using quiz_app.Controllers;
using Xunit;
namespace quiz_app.Tests.Controllers;

public class WeatherForecastControllerTests
{
    private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;
    private readonly WeatherForecastController _controller;

    public WeatherForecastControllerTests()
    {
        _mockLogger = new Mock<ILogger<WeatherForecastController>>();
        _controller = new WeatherForecastController(_mockLogger.Object);
    }

    [Fact]
    public void Get_ReturnsFiveWeatherForecasts()
    {
        // Act
        var result = _controller.Get();

        // Assert
        Assert.IsTrue(result != null, "Result should not be null");
        Assert.IsTrue(result.Count() == 5, "Result should contain exactly 5 items"); 
        foreach (var forecast in result)
        {
            Assert.IsTrue(forecast.TemperatureC >= -20 && forecast.TemperatureC <= 55, "TemperatureC should be in range -20 to 55");
            Assert.IsTrue(new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            }.Contains(forecast.Summary), "Summary should be one of the predefined values"); // Replace Contains with Assert.True
        }
    }
}