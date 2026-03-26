//using GraphQLDemo.Contracts;
//using System.Text;
//using System.Text.Json;

//namespace GraphQLDemo.Serivice
//{
//    public class LLMService : ILLMService
//    {
//        private readonly HttpClient _client;
//        private readonly IConfiguration _config;

//        public LLMService(HttpClient client, IConfiguration config)
//        {
//            _client = client;
//            _config = config;
//        }

//        public async Task<string> Chat(string systemPrompt, string userPrompt)
//        {
//            var apiKey = _config["LLM:ApiKey"];
//            var model = _config["LLM:Model"];
//            var endpoint = _config["LLM:Endpoint"];

//            var body = new
//            {
//                model = model,
//                messages = new[]
//    {
//                new { role = "system", content = systemPrompt },
//                new { role = "user", content = userPrompt }
//            },
//                temperature = 0.2
//            };

//            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);

//            request.Headers.Add("Authorization", $"Bearer {apiKey}");

//            request.Content = new StringContent(
//                JsonSerializer.Serialize(body),
//                Encoding.UTF8,
//                "application/json"
//            );


//            var response = await _client.SendAsync(request);

//            var json = await response.Content.ReadAsStringAsync();

//            var doc = JsonDocument.Parse(json);

//            return doc
//                .RootElement
//                .GetProperty("choices")[0]
//                .GetProperty("message")
//                .GetProperty("content")
//                .GetString();
//        }
//    }
//}




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
            var apiKey = _config["LLM:ApiKey"];
            var model = _config["LLM:Model"];
            var endpoint = _config["LLM:Endpoint"];

            var body = new
            {
                model = model,
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userPrompt }
                },
                temperature = 0.2
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
    }
}