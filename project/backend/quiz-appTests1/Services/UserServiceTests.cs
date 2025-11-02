using Microsoft.VisualStudio.TestTools.UnitTesting;
using quiz_app.Models;
using quiz_app.Services;
using System.Collections.Generic;

namespace quiz_appTests1.Services
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void Register_ShouldAddUser_WhenUsernameAndPasswordAreValid()
        {
            var service = new UserService();
            var result = service.Register("alice", "password123");
            Assert.IsTrue(result);
            var user = service.GetUser("alice");
            Assert.IsNotNull(user);
            Assert.AreEqual("alice", user.username);
        }

        [TestMethod]
        public void Register_ShouldFail_WhenUsernameIsNullOrEmpty()
        {
            var service = new UserService();
            Assert.IsFalse(service.Register(null, "pass"));
            Assert.IsFalse(service.Register("", "pass"));
        }

        [TestMethod]
        public void Register_ShouldFail_WhenPasswordIsNullOrEmpty()
        {
            var service = new UserService();
            Assert.IsFalse(service.Register("bob", null));
            Assert.IsFalse(service.Register("bob", ""));
        }

        [TestMethod]
        public void Register_ShouldFail_WhenUsernameAlreadyExists()
        {
            var service = new UserService();
            service.Register("charlie", "pass1");
            Assert.IsFalse(service.Register("charlie", "pass2"));
        }

        [TestMethod]
        public void Authenticate_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            var service = new UserService();
            service.Register("dave", "secret");
            var user = service.Authenticate("dave", "secret");
            Assert.IsNotNull(user);
            Assert.AreEqual("dave", user.username);
        }

        [TestMethod]
        public void Authenticate_ShouldReturnNull_WhenCredentialsAreIncorrect()
        {
            var service = new UserService();
            service.Register("eve", "1234");
            var user = service.Authenticate("eve", "wrong");
            Assert.IsNull(user);
        }

        [TestMethod]
        public void GetUser_ShouldReturnUser_WhenExists()
        {
            var service = new UserService();
            service.Register("frank", "pass");
            var user = service.GetUser("frank");
            Assert.IsNotNull(user);
            Assert.AreEqual("frank", user.username);
        }

        [TestMethod]
        public void GetUser_ShouldReturnNull_WhenNotExists()
        {
            var service = new UserService();
            var user = service.GetUser("ghost");
            Assert.IsNull(user);
        }

        [TestMethod]
        public void AddSolvedQuiz_ShouldAddQuizResultToUser()
        {
            var service = new UserService();
            service.Register("harry", "pass");
            var result = new QuizResult { CorrectAnswers = 10 };
            service.AddSolvedQuiz("harry", result);
            var quizzes = service.GetSolvedQuizzes("harry");
            Assert.AreEqual(1, quizzes.Count);
            Assert.AreEqual(10, quizzes[0].CorrectAnswers);
            Assert.AreEqual(1, quizzes[0].Id);
        }

        [TestMethod]
        public void GetSolvedQuizzes_ShouldReturnEmptyList_WhenUserNotFound()
        {
            var service = new UserService();
            var quizzes = service.GetSolvedQuizzes("nonexistent");
            Assert.AreEqual(0, quizzes.Count);
        }
    }
}