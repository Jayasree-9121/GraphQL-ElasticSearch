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
            string systemPrompt = $@"
You are an AI debugging and business impact analysis agent.

Application Context:
{AIConstants.MIDContext}

Instructions:
{AIConstants.AIInstructions}

Analyze the given error log and return ONLY valid JSON in this EXACT format:

{{
  ""ErrorType"": """",
  ""ErrorSummary"": """",
  ""PossibleCauses"": [],
  ""OccurrenceConditions"": [],
  ""BusinessImpact"": """",
  ""Severity"": """",
  ""FixSuggestions"": [],
  ""RecommendedFix"": """"
}}

Rules:
- Do NOT use markdown
- Do NOT wrap in ```json
- Do NOT add explanations
- Do NOT change JSON structure
";

            var userPrompt = JsonSerializer.Serialize(log);

            var response = await _llm.Chat(systemPrompt, userPrompt);

            // ✅ Clean LLM response
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
