using quiz_app.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace quiz_app.Services
{
    public class QuizService
    {
        private readonly HttpClient _httpClient;

        public QuizService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuizQuestion>> GetQuestionsAsync(int amount = 5)
        {
            var url = $"https://opentdb.com/api.php?amount={amount}";
            try
            {
                var response = await _httpClient.GetFromJsonAsync<OpenTdbResponse>(url);

                return response?.Results ?? new List<QuizQuestion>();
            }
            catch (JsonException)
            {
                return new List<QuizQuestion>();
            }
        }

        private class OpenTdbResponse
        {
            [JsonPropertyName("response_code")]
            public int ResponseCode { get; set; }

            [JsonPropertyName("results")]
            public List<QuizQuestion> Results { get; set; }
        }
    }
}
