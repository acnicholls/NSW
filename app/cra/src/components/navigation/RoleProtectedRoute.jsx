import React from "react";
import { PropTypes } from "prop-types";
import { Route, Redirect } from "react-router-dom";
import { useUserContext } from "../../contexts/UserContext";

// A wrapper for <Route> that redirects to the login
// route if you're not yet authenticated and have the correct role.
const RoleProtectedRoute = ({ requiredRole, children, ...rest }) => {
  const { user } = useUserContext();
  const showContent =
    user && user.isAuthenticated && user.role === requiredRole;
  return (
    <Route
      {...rest}
      render={({ location }) =>
        showContent ? (
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

export default RoleProtectedRoute;

RoleProtectedRoute.propTypes = {
  requiredRole: PropTypes.string.isRequired,
};
