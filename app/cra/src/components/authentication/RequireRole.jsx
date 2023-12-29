import { useLocation, redirect } from "react-router";
import { useUserContext } from "../../contexts/UserContext";
import routes from "../../constants/RouteConstants";

function RequireRole({ children, selectedRole }) {
  let location = useLocation();
  let { user } = useUserContext();

  if (user && user.isAuthenticated && user.role === selectedRole) {
    return children;
  }
  return redirect(routes.frontend.denied, { from: location });
}

export default RequireRole;
