using Microsoft.VisualStudio.TestTools.UnitTesting;
using quiz_app.Models;
using quiz_app.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace quiz_appTests1.Services
{
    [TestClass]
    public class QuizServiceTests
    {
        private HttpClient CreateMockHttpClient(string jsonResponse)
        {
            var handler = new MockHttpMessageHandler(jsonResponse);
            return new HttpClient(handler);
        }

        [TestMethod]
        public async Task GetQuestionsAsync_ReturnsQuestions_WhenApiReturnsResults()
        {
            var questions = new List<QuizQuestion>
            {
                new QuizQuestion { Question = "Q1", CorrectAnswer = "A1", IncorrectAnswers = new List<string> { "B1", "C1", "D1" } },
                new QuizQuestion { Question = "Q2", CorrectAnswer = "A2", IncorrectAnswers = new List<string> { "B2", "C2", "D2" } }
            };
            var responseObj = new
            {
                response_code = 0,
                results = questions
            };
            string json = JsonSerializer.Serialize(responseObj);

            var httpClient = CreateMockHttpClient(json);
            var service = new QuizService(httpClient);

            var result = await service.GetQuestionsAsync(2);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Q1", result[0].Question);
            Assert.AreEqual("A2", result[1].CorrectAnswer);
        }

        [TestMethod]
        public async Task GetQuestionsAsync_ReturnsEmptyList_WhenApiReturnsNoResults()
        {
            var responseObj = new
            {
                response_code = 0,
                results = new List<QuizQuestion>()
            };
            string json = JsonSerializer.Serialize(responseObj);

            var httpClient = CreateMockHttpClient(json);
            var service = new QuizService(httpClient);

            var result = await service.GetQuestionsAsync(5);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetQuestionsAsync_ReturnsEmptyList_WhenApiResponseIsNull()
        {
            var httpClient = CreateMockHttpClient("");
            var service = new QuizService(httpClient);

            var result = await service.GetQuestionsAsync(5);

            Assert.AreEqual(0, result.Count);
        }

        // Helper mock handler
        private class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly string _response;

            public MockHttpMessageHandler(string response)
            {
                _response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var message = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(_response, Encoding.UTF8, "application/json")
                };
                return Task.FromResult(message);
            }
        }
    }
}