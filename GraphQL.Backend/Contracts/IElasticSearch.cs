namespace GraphQLDemo.Contracts
{
    public interface IElasticSearch
    {
        Task<bool> AddData();
        Task<List<object>> GetData(string text);
    }
}
