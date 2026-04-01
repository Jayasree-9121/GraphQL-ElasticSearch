using GraphQLDemo.Contracts;
using GraphQLDemo.Hellper;
using GraphQLDemo.Models;
using System.Text.Json;

namespace GraphQLDemo.Serivice
{
    public class PlanningAgentService
    {
        private readonly ILLMService _llm;

        public PlanningAgentService(ILLMService llm)
        {
            _llm = llm;
        }

        public async Task<List<PlanningResult>> RunPlanningAgent(ErrorLog log)
        {
            string systemPrompt = @"
You are a senior debugging planning agent.

Strictly return JSON in this format:

[{
  ""ErrorCategory"": """",
  ""Technologies"": [],
  ""InvestigationSteps"": [],
  ""SearchQueries"": []
}]
Do not add explanations.
Avoid deplicates.
";

            var userPrompt = JsonSerializer.Serialize(log);

            var response = await _llm.Chat(systemPrompt, userPrompt);

            response = JsonHelper.CleanJson(response);

            try
            {
                return JsonSerializer.Deserialize<List<PlanningResult>>(response);
            }
            catch
            {
                throw new Exception("Invalid JSON from LLM: " + response);
            }
        }


    }
}