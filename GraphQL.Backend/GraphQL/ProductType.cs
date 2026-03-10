using GraphQL.Types;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class ProductType: ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Price);
        }
    }
}
