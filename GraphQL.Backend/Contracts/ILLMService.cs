namespace GraphQLDemo.Contracts
{
    public interface ILLMService
    {
        Task<string> Chat(string systemPrompt, string userPrompt);
    }
}
