using System.ComponentModel.DataAnnotations;

namespace quiz_app.Models
{
    public class User
    {
        [Required]
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }
        public List<QuizResult> SolvedQuizzes { get; set; } = new();
    }
}
