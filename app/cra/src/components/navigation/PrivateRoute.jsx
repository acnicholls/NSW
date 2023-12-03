import React from "react";
import { Route, Redirect } from "react-router-dom";
import { useUserContext } from "../../contexts/UserContext";
import { anonymousUser } from "../../data/data";

// A wrapper for <Route> that redirects to the login
// route if you're not yet authenticated.
const PrivateRoute = ({ children, ...rest }) => {
  const { user } = useUserContext();
  return (
    <Route
      {...rest}
      render={({ location }) =>
        user && user.isAuthenticated ? (
          children
        ) : (
          <Redirect
            to={{
              pathname: "/login",
              state: { from: location },
            }}
          />
        )
      }
    />
  );
};

export default PrivateRoute;
