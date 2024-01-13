import React, { createContext, useContext, useState, useEffect } from "react";
import PropTypes from "prop-types";
import { anonymousUser } from "../data/data";
import { useNavigate } from "react-router-dom";
import routes from "../constants/RouteConstants";
import { useUserInfo } from "../hooks/userHooks";

const UserContext = createContext(null);

export function useUserContext() {
  return useContext(UserContext);
}

export function UserProvider({ children }) {
  const [user, setUser] = useState(anonymousUser);
  const [userQueryIsDisabled, setUserQueryIsDisabled] = useState(false);
  const [selectedLanguage, setSelectedLanguage] = useState(0);
  const navigate = useNavigate();

  useEffect(() => {
    console.log("User Changed", user);
  }, [user]);

  // const onSuccess = (data) => {
  //   console.log("UserContext.onSuccess: ", data);
  //   setUser(data.data);
  //   setUserQueryIsDisabled(true);
  // };

  // const onError = (error) => {
  //   console.log("UserContext.onError", error);
  //   setUser(anonymousUser);
  //   setUserQueryIsDisabled(true);
  // };

  // useUserInfo(userQueryIsDisabled, onSuccess, onError);

  console.log("in UserContext, languagePreference=:", selectedLanguage);
  useEffect(() => {
    if (selectedLanguage === 0) {
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
