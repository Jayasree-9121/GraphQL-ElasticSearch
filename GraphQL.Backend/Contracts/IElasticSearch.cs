using GraphQLDemo.Models;

namespace GraphQLDemo.Contracts
{
    public interface IElasticSearch
    {
        Task<bool> AddData();
        //Task<List<object>> GetData(string text);

        Task<AIResult> GetData(string text);
    }
}
