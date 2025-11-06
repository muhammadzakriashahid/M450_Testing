using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using quiz_app.Controllers;
using quiz_app.Models;
using quiz_app.Services;

namespace quiz_appTests1.Controllers
{
    [TestClass]
    public class AuthControllerTests
    {
        private Mock<UserServiceDerived> _userServiceMock;
        private AuthController _controller;

        // Derived class to allow overriding non-virtual methods
        public class UserServiceDerived : UserService
        {
            public virtual new bool Register(string? username, string? password)
            {
                return base.Register(username, password);
            }

            public virtual new User? Authenticate(string? username, string? password)
            {
                return base.Authenticate(username, password);
            }
        }

        [TestInitialize]
        public void Setup()
        {
            _userServiceMock = new Mock<UserServiceDerived>() { CallBase = false };
            _controller = new AuthController(_userServiceMock.Object);
        }

        [TestMethod]
        public void Register_ReturnsOk_WhenUserIsRegistered()
        {
            // Arrange
            var request = new LoginRequest { Username = "testuser", Password = "testpass" };
            _userServiceMock.Setup(s => s.Register(request.Username, request.Password)).Returns(true);

            // Act
            var result = _controller.Register(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("User registered", okResult.Value);
        }

        //[TestMethod]
        //public void Register_ReturnsBadRequest_WhenUserAlreadyExists()
        //{
        //    // Arrange
        //    var request = new LoginRequest { Username = "existinguser", Password = "testpass" };
        //    _userServiceMock.Setup(s => s.Register(request.Username, request.Password)).Returns(false);

        //    // Act
        //    var result = _controller.Register(request);

        //    // Assert
        //    var badRequestResult = result as BadRequestObjectResult;
        //    Assert.IsNotNull(badRequestResult, $"Expected BadRequestObjectResult but got {result?.GetType().Name}");
        //    Assert.AreEqual("User already exists", badRequestResult.Value);
        //}

        [TestMethod]
        public void Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var request = new LoginRequest { Username = "testuser", Password = "wrongpass" };
            _userServiceMock.Setup(s => s.Authenticate(request.Username, request.Password)).Returns((User?)null);

            // Act
            var result = _controller.Login(request);

            // Assert
            var unauthorized = result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorized);
            Assert.AreEqual("Invalid credentials", unauthorized.Value);
        }

        //[TestMethod]
        //public void Login_ReturnsOkWithToken_WhenCredentialsAreValid()
        //{
        //    // Arrange
        //    var request = new LoginRequest { Username = "testuser", Password = "testpass" };
        //    var validUser = new User { username = request.Username, password = request.Password };
        //    _userServiceMock.Setup(s => s.Authenticate(request.Username, request.Password)).Returns(validUser);

        //    // Act
        //    var result = _controller.Login(request);

        //    // Assert
        //    var okResult = result as OkObjectResult;
        //    Assert.IsNotNull(okResult, "Result was not OkObjectResult");

        //    // Defensive checks for token
        //    var value = okResult.Value;
        //    Assert.IsNotNull(value, "OkObjectResult.Value was null");

        //    var tokenProp = value.GetType().GetProperty("Token");
        //    Assert.IsNotNull(tokenProp, "Token property not found in result value");

        //    var tokenObj = tokenProp.GetValue(value, null);
        //    Assert.IsNotNull(tokenObj, "Token property value was null");
        //    Assert.IsInstanceOfType(tokenObj, typeof(string));
        //    Assert.IsTrue(!string.IsNullOrEmpty(tokenObj.ToString()), "Token string was empty");
        //}

        [TestMethod]
        public void Login_ReturnsUnauthorized_WhenUsernameIsNull()
        {
            // Arrange
            var request = new LoginRequest { Username = null, Password = "testpass" };
            _userServiceMock.Setup(s => s.Authenticate(null, request.Password)).Returns((User?)null);

            // Act
            var result = _controller.Login(request);

            // Assert
            var unauthorized = result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorized);
            Assert.AreEqual("Invalid credentials", unauthorized.Value);
        }

        [TestMethod]
        public void Register_ReturnsBadRequest_WhenUsernameIsNull()
        {
            // Arrange
            var request = new LoginRequest { Username = null, Password = "testpass" };
            _userServiceMock.Setup(s => s.Register(null, request.Password)).Returns(false);

            // Act
            var result = _controller.Register(request);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, $"Expected BadRequestObjectResult but got {result?.GetType().Name}");
        }
    }
}