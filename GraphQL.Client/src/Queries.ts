import { gql } from '@apollo/client';

export const GET_USERS = gql`
query GetUser($id: Int!) {
  user(id: $id) {
    id
    name
    orders {
      id
      total
    }
    products {
      id
      name
      price
    }
  }
}
`;  