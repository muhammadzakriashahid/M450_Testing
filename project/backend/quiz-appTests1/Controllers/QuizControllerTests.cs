using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using quiz_app.Controllers;
using quiz_app.Models;
using quiz_app.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace quiz_appTests1.Controllers
{
    [TestClass]
    public class QuizControllerTests
    {
        private Mock<QuizService> _quizServiceMock;
        private Mock<UserService> _userServiceMock;

        [TestInitialize]
        public void Setup()
        {
            // Use a mock HttpClient for QuizService
            var httpClient = new HttpClient(new HttpMessageHandlerStub());
            _quizServiceMock = new Mock<QuizService>(httpClient) { CallBase = true };
            _userServiceMock = new Mock<UserService>() { CallBase = true };
        }

        // Helper stub for HttpClient
        private class HttpMessageHandlerStub : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var questions = new List<QuizQuestion>
        {
            new QuizQuestion { Question = "Q1", CorrectAnswer = "A1", IncorrectAnswers = new List<string> { "B1", "C1" } }
        };
                var responseObj = new { response_code = 0, results = questions };
                var json = System.Text.Json.JsonSerializer.Serialize(responseObj);
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                return Task.FromResult(message);
            }
        }

        private QuizController CreateControllerWithUser(string username = null)
        {
            var controller = new QuizController(_quizServiceMock.Object, _userServiceMock.Object);

            if (username != null)
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }, "mock"));
                controller.ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                };
            }
            else
            {
                controller.ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                };
            }

            return controller;
        }

        [TestMethod]
        public async Task Get_ReturnsQuestionsList()
        {
            var controller = CreateControllerWithUser();
            var result = await controller.Get();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var questions = okResult.Value as List<QuizQuestion>;
            Assert.IsNotNull(questions);
            Assert.AreEqual("Q1", questions[0].Question);
            Assert.AreEqual("A1", questions[0].CorrectAnswer);
        }

        [TestMethod]
        public void MarkQuizSolved_ReturnsUnauthorized_WhenUserIsNotAuthenticated()
        {
            var quizResult = new QuizResult { CorrectAnswers = 2 };
            var controller = CreateControllerWithUser();

            var result = controller.MarkQuizSolved(quizResult);

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        // Derived class to allow overriding non-virtual methods
        public class UserServiceDerived : UserService
        {
            public virtual new List<QuizResult> GetSolvedQuizzes(string? username)
            {
                return base.GetSolvedQuizzes(username);
            }
        }


        [TestMethod]
        public void GetSolvedQuizzes_ReturnsUnauthorized_WhenUserIsNotAuthenticated()
        {
            var controller = CreateControllerWithUser();

            var result = controller.GetSolvedQuizzes();

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }
    }
}