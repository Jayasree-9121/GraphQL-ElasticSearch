using GraphQL.Types;
using GraphQLDemo.Contracts;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id);
            Field(x => x.Name);

            Field<ListGraphType<OrderType>>(
                "orders",
                resolve: context =>
                {
                    var userService = context.RequestServices.GetRequiredService<IUserService>();
                    var user = context.Source;

                    return userService.GetOrdersByUserId(user.Id);
                }
            );

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context =>
                {
                    var userService = context.RequestServices.GetRequiredService<IUserService>();
                    var user = context.Source;

                    return userService.GetProductsByUserId(user.Id);
                }
            );
        }
    }
}