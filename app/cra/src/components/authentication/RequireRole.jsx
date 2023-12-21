import { useLocation, useNavigate } from "react-router";
import { useUserInfo } from "../../hooks/userHooks";
import routes from "../../constants/RouteConstants";

function RequireRole({ children, selectedRole }) {
  let location = useLocation();
  let { user } = useUserInfo();
  let navigate = useNavigate();

  if (user && !user.isAuthenticated && user.role !== selectedRole) {
    return navigate(routes.frontend.denied, { from: location });
  }

  return children;
}

export default RequireRole;
