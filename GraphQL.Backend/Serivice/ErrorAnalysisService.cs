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

        public async Task<List<ErrorAnalysisResult>> Analyze(ErrorLog log)
        {
            string systemPrompt = $@"You are an AI debugging and business impact analysis agent.

Application Context:
{{AIConstants.MIDContext}}

Instructions:
{{AIConstants.AIInstructions}}

Analyze the given error log and identify the root cause, especially focusing on API, GraphQL schema mismatches, and backend return types.

Return ONLY valid JSON as a LIST of objects in this EXACT format:

[
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
]

Rules:
- Do NOT use markdown
- Do NOT wrap in ```json
- Do NOT add explanations
- Do NOT change JSON structure
- Always return a JSON array (even if only one error)
- Ensure all fields are present
- PossibleCauses, OccurrenceConditions, and FixSuggestions must be arrays of strings
- Severity must be one of: ""Low"", ""Medium"", ""High"", ""Critical""
- Focus on identifying mismatches between GraphQL schema and resolver return types
- Include actionable backend and schema-level fixes in FixSuggestions and RecommendedFix
- Avoid Deplicates

Input:
{{error_logs}}";

            var userPrompt = JsonSerializer.Serialize(log);

            var response = await _llm.Chat(systemPrompt, userPrompt);


            response = JsonHelper.CleanJson(response);

            try
            {
                return JsonSerializer.Deserialize<List<ErrorAnalysisResult>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("ErrorAnalysis JSON parse failed: " + response, ex);
            }
        }
    }
}
