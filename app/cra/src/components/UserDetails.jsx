import React from "react";
import { useUserContext } from "../contexts/UserContext";

const UserDetails = () => {
  const { user } = useUserContext();
  return (
    <>
      <p>{"This is the user details page."}</p>
      <ul>
        <li>UserId: {user.id}</li>
        <li>Username: {user.username}</li>
        <li>First Name: {user.firstName}</li>
        <li>Last Name: {user.lastName}</li>
        <li>Website: {user.website}</li>
      </ul>
    </>
  );
};

export default UserDetails;
