using GraphQL.Types;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            Field(x => x.Id);
            Field(x => x.Total);
        }
    }
}
