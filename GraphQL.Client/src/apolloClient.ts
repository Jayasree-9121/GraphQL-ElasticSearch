import { ApolloClient, InMemoryCache, createHttpLink } from "@apollo/client";

const httpLink = createHttpLink({
  uri: "https://localhost:7020/graphql", 
});

const client = new ApolloClient({
  link: httpLink,     
  cache: new InMemoryCache(),
});

export default client;