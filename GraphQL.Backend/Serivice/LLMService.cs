using GraphQLDemo.Contracts;
using System.Text;
using System.Text.Json;

namespace GraphQLDemo.Serivice
{
    public class LLMService : ILLMService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public LLMService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<string> Chat(string systemPrompt, string userPrompt)
        {
            try
            {
                var apiKey = _config["LLM:ApiKey"];
                var model = _config["LLM:Model"];
                var endpoint = _config["LLM:Endpoint"];

                if (userPrompt.Length > 4000)
                {
                    userPrompt = userPrompt.Substring(0, 4000);
                }

                var body = new
                {
                    model = model,
                    messages = new[]
                    {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userPrompt }
                },
                    temperature = 0.2,
                    max_tokens = 600,
                    top_p = 0.9
                };

                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                request.Headers.Add("Authorization", $"Bearer {apiKey}");

                request.Content = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"LLM API failed: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();

                var doc = JsonDocument.Parse(json);

                return doc
                    .RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }
    }
}