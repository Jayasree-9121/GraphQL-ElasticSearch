using GraphQL;
using GraphQL.Types;
using GraphQL.Server.Ui.Playground;
using GraphQLDemo.Contracts;
using GraphQLDemo.GraphQL;
using GraphQLDemo.Serivice;

var builder = WebApplication.CreateBuilder(args);


// Application Services (Scoped)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IElasticSearch, ElasticSearch>();


// GraphQL Types (Singleton)
builder.Services.AddSingleton<UserType>();
builder.Services.AddSingleton<OrderType>();
builder.Services.AddSingleton<ProductType>();
builder.Services.AddSingleton<AppInsightsType>();

// GraphQL Query & Mutation (Singleton)
builder.Services.AddSingleton<AppQuery>();
builder.Services.AddSingleton<AppMutation>();


// GraphQL Schema (Singleton)
builder.Services.AddSingleton<ISchema, AppSchema>();


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// GraphQL Configuration
builder.Services.AddGraphQL(builder =>
{
    builder
        .AddSystemTextJson()
        .AddSchema<AppSchema>()
        .AddErrorInfoProvider(opt =>
        {
            opt.ExposeExceptionDetails = true;
        });
});

var app = builder.Build();


// Middleware
app.UseCors("AllowAll");


// GraphQL Endpoint
app.UseGraphQL<ISchema>("/graphql");


// GraphQL Playground
app.UseGraphQLPlayground("/playground");


app.Run();