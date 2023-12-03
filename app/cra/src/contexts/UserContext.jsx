import React, { createContext, useContext, useState } from "react";
import PropTypes from "prop-types";
import routes from "../constants/RouteConstants";
import { useApiContext } from "./ApiContext";
import { anonymousUser } from "../data/data";

const UserContext = createContext(null);

export function useUserContext() {
  return useContext(UserContext);
}

export function UserProvider({ children }) {
  const [user, setUser] = useState(anonymousUser);
  const api = useApiContext();

  const loggedIn = async () => {
    try {
      var userInfo = await api.apiGet(routes.userInfo);
      console.log("LoggedIn:userInfo:", userInfo);
      if (userInfo && userInfo.status === 200) {
        setUser(userInfo.data);
      } else {
        setUser(anonymousUser);
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <UserContext.Provider value={{ user, setUser, loggedIn }}>
      {children}
    </UserContext.Provider>
  );
}

UserProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
