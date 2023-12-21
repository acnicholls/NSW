import React, { createContext, useContext, useState, useEffect } from "react";
import PropTypes from "prop-types";

import { anonymousUser } from "../data/data";

const UserContext = createContext(null);

export function useUserContext() {
  return useContext(UserContext);
}

export function UserProvider({ children }) {
  const [user, setUser] = useState(anonymousUser);

  useEffect(() => {
    console.log("User Changed", user);
  }, [user]);

  return (
    <UserContext.Provider value={{ user, setUser }}>
      {children}
    </UserContext.Provider>
  );
}

UserProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
