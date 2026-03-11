using GraphQLDemo.Contracts;
using GraphQLDemo.Models;

namespace GraphQLDemo.Serivice
{
    public class UserService : IUserService
    {
        private IElasticSearch _ElasticSearch;
        public UserService(IElasticSearch elasticSearch) {
            _ElasticSearch = elasticSearch;
        }
        private readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "Nithin" },
        new User { Id = 2, Name = "Rahul" }
    };

        private readonly List<Order> _orders = new()
    {
        new Order { Id = 101, Total = 5000, UserId = 1 },
        new Order { Id = 102, Total = 7000, UserId = 1 },
        new Order { Id = 103, Total = 3000, UserId = 2 }
    };

        private readonly List<Product> _products = new()
{
    new Product { Id = 1, Name = "Laptop", Price = 70000, UserId = 1 },
    new Product { Id = 2, Name = "Mobile", Price = 20000, UserId = 1 },
    new Product { Id = 3, Name = "Tablet", Price = 15000, UserId = 2 }
};

        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _orders.Where(x => x.UserId == userId).ToList();
        }


        public List<Product> GetProductsByUserId(int userId)
        {
            return _products.Where(x => x.UserId == userId).ToList();
        }

        public User CreateUser(string name)
        {
            var newUser = new User
            {
                Id = _users.Max(x => x.Id) + 1,
                Name = name
            };

            _users.Add(newUser);
            return newUser;
        }

        public Order CreateOrder(int userId, decimal total)
        {
            var newOrder = new Order
            {
                Id = _orders.Max(x => x.Id) + 1,
                UserId = userId,
                Total = total
            };

            _orders.Add(newOrder);
            return newOrder;
        }
    }
}
