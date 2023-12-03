import { useEffect } from "react";
import { useUserContext } from "../../contexts/UserContext";
import { anonymousUser } from "../../data/data";

const LoggedIn = () => {
  const { user, loggedIn } = useUserContext();

  useEffect(() => {
    if (user === null || user === anonymousUser) {
      const callLoggedIn = async () => {
        await loggedIn();
      };
      callLoggedIn();
    }
  });
  return null;
};

export default LoggedIn;
