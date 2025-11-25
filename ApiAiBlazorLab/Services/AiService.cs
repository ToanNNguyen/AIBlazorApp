using System.Net.Http.Json;
using System.Text.Json;

namespace ApiAiBlazorLab.Services
{
    public class AiService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;

        public AiService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["OpenAI:ApiKey"] ?? "";
        }

        public async Task<string> AskAsync(string prompt)
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
                return "API key not configured.";

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            _http.DefaultRequestHeaders.Add("HTTP-Referer", "https://openrouter.ai");
            _http.DefaultRequestHeaders.Add("X-Title", "ApiAiBlazorLab");

            var body = new
            {
                model = "x-ai/grok-4.1-fast:free",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var response = await _http.PostAsJsonAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                body);

            if (!response.IsSuccessStatusCode)
                return $"Error: {response.StatusCode}";

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()
                ?? "(No response)";
        }
    }
}
