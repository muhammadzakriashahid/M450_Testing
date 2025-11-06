using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quiz_app.Models;
using quiz_app.Services;

namespace quiz_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;
        private readonly UserService _userService;

        public QuizController(QuizService quizService, UserService userService)
        {
            _quizService = quizService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuizQuestion>>> Get([FromQuery] int amount = 5)
        {
            var questions = await _quizService.GetQuestionsAsync(amount);
            return Ok(questions);
        }

        [Authorize]
        [HttpPost("solved")]
        public IActionResult MarkQuizSolved([FromBody] QuizResult result)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username)) return Unauthorized();

            // avoid relying on frontend
            if (result != null)
            {
                if (result.TotalQuestions > 0)
                {
                    result.Percentage = Math.Round(result.CorrectAnswers / (double)result.TotalQuestions * 100.0, 4);
                }
                else
                {
                    result.Percentage = 0;
                }

                if (result.CompletedAt == default)
                {
                    result.CompletedAt = DateTime.UtcNow;
                }
            }

            _userService.AddSolvedQuiz(username, result);
            return Ok();
        }

        [Authorize]
        [HttpGet("solved")]
        public IActionResult GetSolvedQuizzes()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username)) return Unauthorized();

            var quizzes = _userService.GetSolvedQuizzes(username);
            return Ok(quizzes);
        }
    }
}