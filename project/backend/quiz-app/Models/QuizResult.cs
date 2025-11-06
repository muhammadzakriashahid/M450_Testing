namespace quiz_app.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double Percentage { get; set; }
        public DateTime CompletedAt { get; set; }
        public List<QuizAnswer> Answers { get; set; } = new();
    }
}
