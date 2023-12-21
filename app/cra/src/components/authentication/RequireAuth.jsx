import { useLocation, useNavigate } from "react-router";
import { useUserInfo } from "../../hooks/userHooks";
import routes from "../../constants/RouteConstants";

function RequireAuth({ children }) {
  let location = useLocation();
  let { user } = useUserInfo();
  let navigate = useNavigate();

  if (user && !user.isAuthenticated) {
    return navigate(routes.frontend.denied, { from: location });
  }

  return children;
}

export default RequireAuth;
