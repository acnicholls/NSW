import { useUserContext } from "../../contexts/UserContext";
import { anonymousUser } from "../../data/data";

const LoggedOut = () => {
  const { user, setUser } = useUserContext();

  setUser(anonymousUser);
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
