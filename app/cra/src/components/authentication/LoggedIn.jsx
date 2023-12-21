import { useUserContext } from "../../contexts/UserContext";
import { useNavigate } from "react-router-dom";
import { useUserInfo } from "../../hooks/userHooks";
import routes from "../../constants/RouteConstants";

const LoggedIn = () => {
  const { user, setUser } = useUserContext();
  const navigate = useNavigate();

  const onSuccess = (response) => {
    console.log("LoggedIn onSuccess");
    console.log(response);
    setUser(response.data);
    navigate(routes.frontend.myPosts);
  };

  const onError = (error) => {
    console.log("LoggedIn onError");
    console.log(error);
  };

  var queryIsDisabled = user.id > 0;
  useUserInfo(queryIsDisabled, onSuccess, onError);

  console.log("LoggedIn.User", user);
  return null;
};

export default LoggedIn;
