using quiz_app.Models;

namespace quiz_app.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();
        private int _nextQuizId = 1;

        public UserService()
        {
            // Add default user
            _users.Add(new User { username = "peterpan", password = "foobar" });
        }

        public bool Register(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || _users.Any(u => u.username == username)) return false;
            _users.Add(new User { username = username, password = password });
            return true;
        }

        public User? Authenticate(string? username, string? password)
        {
            return _users.FirstOrDefault(u => u.username == username && u.password == password);
        }

        public User? GetUser(string? username)
        {
            return _users.FirstOrDefault(u => u.username == username);
        }

        public void AddSolvedQuiz(string? username, QuizResult result)
        {
            var user = GetUser(username);
            if (user != null)
            {
                result.Id = _nextQuizId++;
                user.SolvedQuizzes.Add(result);
            }
        }

        public List<QuizResult> GetSolvedQuizzes(string? username)
        {
            var user = GetUser(username);
            return user?.SolvedQuizzes ?? new List<QuizResult>();
        }
    }
}
