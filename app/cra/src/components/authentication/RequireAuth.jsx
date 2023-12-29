import { useLocation, redirect } from "react-router";
import { useUserContext } from "../../contexts/UserContext";
import routes from "../../constants/RouteConstants";

function RequireAuth({ children }) {
  let location = useLocation();
  let { user } = useUserContext();

  console.log("RequireAuth.user", user);
  if (user && user.isAuthenticated) {
    return children;
  }
  return redirect(routes.frontend.denied, { from: location });
}

export default RequireAuth;
