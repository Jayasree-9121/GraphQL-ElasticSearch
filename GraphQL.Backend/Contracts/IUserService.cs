
using GraphQLDemo.Models;

namespace GraphQLDemo.Contracts
{
    public interface IUserService
    {
        User GetUserById(int id);
        List<Order> GetOrdersByUserId(int userId);
        List<Product> GetProductsByUserId(int userId);
        User CreateUser(string name);
        Order CreateOrder(int userId, decimal total);
    }
}
