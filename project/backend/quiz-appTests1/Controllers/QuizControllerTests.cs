// csharp
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using quiz_app.Controllers;
using quiz_app.Models;
using quiz_app.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        private QuizController CreateControllerWithUser(string username = null, bool authenticatedButNoName = false)
        {
            var controller = new QuizController(_quizServiceMock.Object, _userServiceMock.Object);

            if (authenticatedButNoName)
            {
                // Authenticated identity but no Name claim -> Identity.IsAuthenticated == true, Name == null
                var identity = new ClaimsIdentity(new Claim[] { }, "mock");
                var user = new ClaimsPrincipal(identity);
                controller.ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                };
            }
            else if (username != null)
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
        public void MarkQuizSolved_ReturnsUnauthorized_WhenAuthenticatedButNameMissing()
        {
            // arrange: authenticated identity but no Name claim
            var controller = CreateControllerWithUser(authenticatedButNoName: true);
            var quizResult = new QuizResult { TotalQuestions = 5, CorrectAnswers = 3 };

            // act
            var result = controller.MarkQuizSolved(quizResult);

            // assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void MarkQuizSolved_SetsPercentageToZero_WhenTotalQuestionsZero()
        {
            // arrange
            var username = "zeroUser";
            _userServiceMock.Object.Register(username, "pass");
            var controller = CreateControllerWithUser(username);

            var before = DateTime.UtcNow;
            var quizResult = new QuizResult
            {
                TotalQuestions = 0,
                CorrectAnswers = 0,
                Answers = new List<QuizAnswer>()
            };

            // act
            var postResult = controller.MarkQuizSolved(quizResult);
            Assert.IsInstanceOfType(postResult, typeof(OkResult), "MarkQuizSolved should return Ok for authenticated user");

            var getResult = controller.GetSolvedQuizzes();
            var ok = getResult as OkObjectResult;
            Assert.IsNotNull(ok, "GetSolvedQuizzes should return OkObjectResult");

            var saved = ok.Value as List<QuizResult>;
            Assert.IsNotNull(saved, "Saved quizzes list should not be null");
            Assert.IsTrue(saved.Count > 0, "There should be at least one saved QuizResult");

            var latest = saved[saved.Count - 1];

            // assert percentage set to 0 for TotalQuestions == 0
            Assert.AreEqual(0.0, latest.Percentage, 0.0001, "Percentage should be 0 when TotalQuestions is 0");

            // CompletedAt should be set to a recent time (non-default)
            Assert.IsTrue(latest.CompletedAt != default(DateTime), "CompletedAt should be set");
            var after = DateTime.UtcNow;
            Assert.IsTrue(latest.CompletedAt.ToUniversalTime() >= before.ToUniversalTime(), "CompletedAt should be >= before time");
            Assert.IsTrue(latest.CompletedAt.ToUniversalTime() <= after.ToUniversalTime().AddSeconds(1), "CompletedAt should be recent");
        }

         [TestMethod]
        public void QuizAnswer_Defaults_AreEmptyAndFalse()
        {
            // arrange & act
            var answer = new QuizAnswer();

            // assert defaults
            Assert.AreEqual(string.Empty, answer.Question, "Default Question should be empty string");
            Assert.AreEqual(string.Empty, answer.Category, "Default Category should be empty string");
            Assert.AreEqual(string.Empty, answer.Difficulty, "Default Difficulty should be empty string");
            Assert.AreEqual(string.Empty, answer.CorrectAnswer, "Default CorrectAnswer should be empty string");
            Assert.AreEqual(string.Empty, answer.UserAnswer, "Default UserAnswer should be empty string");
            Assert.IsFalse(answer.IsCorrect, "Default IsCorrect should be false");
        }

        [TestMethod]
        public void QuizAnswer_CanSetAndGetProperties()
        {
            // arrange
            var answer = new QuizAnswer
            {
                Question = "What is 2+2?",
                Category = "Math",
                Difficulty = "Easy",
                CorrectAnswer = "4",
                UserAnswer = "4",
                IsCorrect = true
            };

            // act & assert
            Assert.AreEqual("What is 2+2?", answer.Question);
            Assert.AreEqual("Math", answer.Category);
            Assert.AreEqual("Easy", answer.Difficulty);
            Assert.AreEqual("4", answer.CorrectAnswer);
            Assert.AreEqual("4", answer.UserAnswer);
            Assert.IsTrue(answer.IsCorrect);
        }
        [TestMethod]
        public async Task Get_ReturnsQuestions_FromQuizService()
        {
            // arrange
            var controller = CreateControllerWithUser();
            var amount = 1;

            // act
            var actionResult = await controller.Get(amount);

            // assert - should return OkObjectResult with list of questions from the stubbed handler
            var ok = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(ok, "Get should return OkObjectResult");
            var list = ok.Value as List<QuizQuestion>;
            Assert.IsNotNull(list, "Returned value should be a list of QuizQuestion");
            Assert.AreEqual(amount, list.Count, "Number of questions returned should match requested amount (stub returns 1)");
        }

        [TestMethod]
        public void MarkQuizSolved_ComputesPercentage_WhenTotalQuestionsPositive()
        {
            // arrange
            var username = "percentUser";
            _userServiceMock.Object.Register(username, "pass");
            var controller = CreateControllerWithUser(username);

            var quizResult = new QuizResult
            {
                TotalQuestions = 3,
                CorrectAnswers = 2,
                Answers = new List<QuizAnswer>()
            };

            var expected = Math.Round(quizResult.CorrectAnswers / (double)quizResult.TotalQuestions * 100.0, 4);

            // act
            var postResult = controller.MarkQuizSolved(quizResult);
            Assert.IsInstanceOfType(postResult, typeof(OkResult), "MarkQuizSolved should return Ok for authenticated user");

            var getResult = controller.GetSolvedQuizzes();
            var ok = getResult as OkObjectResult;
            Assert.IsNotNull(ok, "GetSolvedQuizzes should return OkObjectResult");

            var saved = ok.Value as List<QuizResult>;
            Assert.IsNotNull(saved, "Saved quizzes list should not be null");
            Assert.IsTrue(saved.Count > 0, "There should be at least one saved QuizResult");

            var latest = saved[saved.Count - 1];

            // assert percentage calculated and stored correctly
            Assert.AreEqual(expected, latest.Percentage, 0.0001, "Percentage should be computed and rounded correctly when TotalQuestions > 0");
        }
    }
}