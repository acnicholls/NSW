import { useUserContext } from "../../contexts/UserContext";
import initialUser from "../../constants/user";

const LoggedOut = () => {
  const { user, setUser } = useUserContext();

  setUser(initialUser);
  const returnedView = user.isAuthenticated ? (
    <>
      Houston, we have a problem. Please contact your nearest webmaster and get
      them to attempt to debug the issue.
    </>
  ) : (
    <>You have successfully logged out.</>
  );
  return <>{returnedView}</>;
};

export default LoggedOut;
