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
    public class QuizControllerTest
    {
        private Mock<QuizService> _quizServiceMock;
        private Mock<UserService> _userServiceMock;

        [TestInitialize]
        public void Setup()
        {
            var httpClient = new HttpClient(new HttpMessageHandlerStub());
            _quizServiceMock = new Mock<QuizService>(httpClient) { CallBase = true };
            _userServiceMock = new Mock<UserService>() { CallBase = true };
        }

        // stubbed HTTP handler to satisfy QuizService ctor/use if needed
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
        public void MarkQuizSolved_ReturnsUnauthorized_WhenUserIsNotAuthenticated()
        {
            var quizResult = new QuizResult { CorrectAnswers = 2 };
            var controller = CreateControllerWithUser();

            var result = controller.MarkQuizSolved(quizResult);

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void GetSolvedQuizzes_ReturnsUnauthorized_WhenUserIsNotAuthenticated()
        {
            var controller = CreateControllerWithUser();

            var result = controller.GetSolvedQuizzes();

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void MarkQuizSolved_Persists_TotalQuestions_Percentage_And_CompletedAt()
        {
            // arrange
            var username = "testuser";
            _userServiceMock.Object.Register(username, "pass");
            var controller = CreateControllerWithUser(username);
            var nowBefore = DateTime.UtcNow;

            var quizResult = new QuizResult
            {
                TotalQuestions = 10,
                CorrectAnswers = 7,
                Answers = new List<QuizAnswer>()
            };

            var postResult = controller.MarkQuizSolved(quizResult);
            Assert.IsInstanceOfType(postResult, typeof(OkResult), "MarkQuizSolved should return Ok for authenticated user");

            var getResult = controller.GetSolvedQuizzes();
            var ok = getResult as OkObjectResult;
            Assert.IsNotNull(ok, "GetSolvedQuizzes should return OkObjectResult");

            var saved = ok.Value as List<QuizResult>;
            Assert.IsNotNull(saved, "Saved quizzes list should not be null");
            Assert.IsTrue(saved.Count > 0, "There should be at least one saved QuizResult");

            var latest = saved[saved.Count - 1];

            Assert.AreEqual(10, latest.TotalQuestions, "TotalQuestions should be preserved");
            Assert.AreEqual(70.0, latest.Percentage, 0.0001, "Percentage should be computed correctly");

            var nowAfter = DateTime.UtcNow;
            Assert.IsTrue(latest.CompletedAt.ToUniversalTime() >= nowBefore.ToUniversalTime(), "CompletedAt should be >= time before call");
            Assert.IsTrue(latest.CompletedAt.ToUniversalTime() <= nowAfter.ToUniversalTime().AddSeconds(1), "CompletedAt should be set to a recent time");
        }
    }
}