import { useQuery } from "@apollo/client/react";
import { GET_USERS } from "./Queries";
import { useEffect, useState } from "react";



interface Order {
  id: number;
  total: number;
}

interface Product {
  id: number;
  name: string;
}

interface User {
  id: number;
  name: string;
  orders: Order[];
  products: Product[];
}

interface GetUsersResponse {
  user: User;
}

interface GetUserVariables {
  id: number;
}

function UserList() {
  const { loading, error, data } =

    useQuery<GetUsersResponse, GetUserVariables>(
      GET_USERS,
      {
        variables: { id: 2 },
      }
    );

  const [userData, setUserData] = useState<User>();
  const [loader, setLoader] = useState(true);
  const [errors, setErrors] = useState(null);


  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  const user = data?.user;

  // useEffect(() => {
  //   fetchUserData(2);
  // }, [])

  const fetchUserData = async (id: number) => {
    try {
      const response = await fetch('https://localhost:7020/graphql', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          query: `
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
        `,
          variables: { id }
        }),
      });

      const result = await response.json();

      if (result.errors) {
        setErrors(result.errors);
        return;
      }

      setUserData(result.data.user);
      setLoader(false);
      console.log(result.data.user);
    } catch (error) {
      console.error("Fetch error:", error);
    }
  };

  return (
    <div>

      <h1>Appolo client</h1>
      <h2>{user?.name}</h2>

      <h3>Orders</h3>
      {user?.orders.map(order => (
        <div key={order.id}>
          {order.id} - {order.total}
        </div>
      ))}

      <h3>Products</h3>
      {user?.products.map(product => (
        <div key={product.id}>
          {product.name}
        </div>
      ))}


      <h1>Fetch</h1>
      <h2>{userData?.name}</h2>
      
      <h3>Orders</h3>
      {userData?.orders.map(order => (
        <div key={order.id}>
          {order.id} - {order.total}
        </div>
      ))}

      <h3>Products</h3>
      {userData?.products.map(product => (
        <div key={product.id}>
          {product.name}
        </div>
      ))}
    </div>
  );
}

export default UserList;