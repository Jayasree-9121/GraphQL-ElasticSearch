using GraphQLDemo.Contracts;
using GraphQLDemo.Hellper;
using GraphQLDemo.Models;
using System.Text.Json;

namespace GraphQLDemo.Serivice
{
    public class ErrorAnalysisService
    {
        private readonly ILLMService _llm;

        public ErrorAnalysisService(ILLMService llm)
        {
            _llm = llm;
        }

        public async Task<ErrorAnalysisResult> Analyze(ErrorLog log)
        {
            string systemPrompt = @"
You are an expert software debugging AI.

Analyze the error log and provide:

- error_type
- error_summary
- possible_causes
- occurrence_conditions
- fix_suggestions
- recommended_fix

Return JSON only.
";

            var userPrompt = JsonSerializer.Serialize(log);

            var response = await _llm.Chat(systemPrompt, userPrompt);

            response = JsonHelper.CleanJson(response);

            try
            {
                return JsonSerializer.Deserialize<ErrorAnalysisResult>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("ErrorAnalysis JSON parse failed: " + response, ex);
            }
        }
    }
}
