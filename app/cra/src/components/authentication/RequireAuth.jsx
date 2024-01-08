import { Navigate } from "react-router";
import { useUserContext } from "../../contexts/UserContext";
import routes from "../../constants/RouteConstants";

function RequireAuth({ children }) {
  let { user } = useUserContext();

  console.log("RequireAuth.user", user);
  if (user && user.isAuthenticated) {
    return children;
  }
  return <Navigate to={routes.frontend.denied} />;
}

export default RequireAuth;
