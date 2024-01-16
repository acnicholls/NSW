import React, { createContext, useContext, useState, useEffect } from "react";
import PropTypes from "prop-types";
import { anonymousUser } from "../data/data";
import { useNavigate, useLocation } from "react-router-dom";
import routes from "../constants/RouteConstants";
import { useUserInfo } from "../hooks/userHooks";
import { useCookies } from "react-cookie";

const UserContext = createContext(null);

export function useUserContext() {
  return useContext(UserContext);
}

export function UserProvider({ children }) {
  const [user, setUser] = useState(anonymousUser);
  const [selectedLanguage, setSelectedLanguage] = useState(0);
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    console.log("User Changed", user);
  }, [user]);

  console.log("in UserContext, languagePreference=:", selectedLanguage);
  useEffect(() => {
    if (
      selectedLanguage === 0 &&
      location.pathname !== routes.frontend.denied
    ) {
      navigate(routes.frontend.splash);
    }
  });

  return (
    <UserContext.Provider
      value={{ user, setUser, selectedLanguage, setSelectedLanguage }}
    >
      {children}
    </UserContext.Provider>
  );
}

UserProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
