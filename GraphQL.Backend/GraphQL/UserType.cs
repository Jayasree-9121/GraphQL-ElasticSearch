using GraphQL.Types;
using GraphQLDemo.Contracts;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(IUserService userService)
        {
            Field(x => x.Id);
            Field(x => x.Name);

            Field<ListGraphType<OrderType>>(
                "orders",
                resolve: context =>
                {
                    var user = context.Source;
                    return userService.GetOrdersByUserId(user.Id);
                }
            );

            Field<ListGraphType<ProductType>>(
    "products",
    resolve: context =>
    {
        var user = context.Source;
        return userService.GetProductsByUserId(user.Id);
    }
);
        }
    }
}
