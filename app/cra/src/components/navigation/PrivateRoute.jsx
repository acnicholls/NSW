import React from "react";
import { Route, Redirect } from "react-router-dom";
import { useUserContext } from "../../contexts/UserContext";

// A wrapper for <Route> that redirects to the login
// route if you're not yet authenticated.
const PrivateRoute = ({ children, ...rest }) => {
  // let auth = useAuth();
  const { user } = useUserContext();
  return (
    <Route
      {...rest}
      render={({ location }) =>
        user ? (
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
