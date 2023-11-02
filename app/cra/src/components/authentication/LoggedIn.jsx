import { useEffect } from "react";
import { useUserContext } from "../../contexts/UserContext";

const LoggedIn = () => {
  const { user, loggedIn } = useUserContext();

  useEffect(() => {
    if (user === null) {
      const callLoggedIn = async () => {
        await loggedIn();
      };
      callLoggedIn();
    }
  });
  return null;
};

export default LoggedIn;
