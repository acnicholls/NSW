import React from "react";
import {
  // BrowserRouter as Router,
  // Switch,
  // Route,
  // Link,
  NavLink,
  // Redirect,
  // useHistory,
  // useLocation
} from "react-router-dom";
import { useUserContext } from "../../contexts/UserContext";

const NavBar = () => {
  const { user } = useUserContext();
  // const auth = useAuth();
  // const handleLoginClick = () => {
  //   auth.signin();
  // }
  const loggedOutView = (
    <>
      <table>
        <tbody>
          <tr>
            <td>
              <NavLink to="/index">Index</NavLink>
            </td>
            <td>
              <NavLink to="/about">About</NavLink>
            </td>
            <td>
              <NavLink to="/register">Register</NavLink>
            </td>
            <td>
              <NavLink to="/login">Login</NavLink>
            </td>
          </tr>
        </tbody>
      </table>
    </>
  );

  const loggedInView = (
    <>
      <table>
        <tbody>
          <tr>
            <td>
              <NavLink to="/index">Index</NavLink>
            </td>
            <td>
              <NavLink to="/about">About</NavLink>
            </td>
            <td>
              <NavLink to="/user-details">Profile</NavLink>
            </td>
            <td>
              <NavLink to="/logout">Log out</NavLink>
            </td>
          </tr>
        </tbody>
      </table>
    </>
  );

  const userView = user ? loggedInView : loggedOutView;
  return <>{userView}</>;
};

export default NavBar;
