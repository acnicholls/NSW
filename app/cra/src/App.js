// import logo from './logo.svg';
import "./App.css";

import React from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect,
  // Link,
  // Redirect
} from "react-router-dom";
import About from "./components/About";
import Index from "./components/Index";
import UserDetails from "./components/UserDetails";
// import { ProvideAuth } from "./contexts/AuthContext_cookie";
// import Login from "./components/authentication/Login";
import PrivateRoute from "./components/navigation/PrivateRoute";
import NavBar from "./components/navigation/NavBar";
import { CookiesProvider } from "react-cookie";
import { ApiProvider } from "./contexts/ApiContext";
// import Logout from "./components/authentication/Logout";
import LoggedIn from "./components/authentication/LoggedIn";
import LoggedOut from "./components/authentication/LoggedOut";
import { UserProvider } from "./contexts/UserContext";
import { WeatherProvider } from "./contexts/WeatherContext";
import ExternalRedirect from "./components/navigation/ExternalRedirect";
import routes from "./constants/RouteConstants";

export default function App() {
  return (
    <CookiesProvider>
      <ApiProvider>
        <UserProvider>
          <Router>
            <NavBar />
            <div>
              <Switch>
                <Route exact path="/">
                  <Redirect to="/index" />
                </Route>
                <Route path="/index">
                  <Index />
                </Route>
                <Route path="/about">
                  <WeatherProvider>
                    <About />
                  </WeatherProvider>
                </Route>
                <ExternalRedirect
                  path="/login"
                  link={`${routes.login}`}
                  exact={true}
                />
                <ExternalRedirect
                  path="/logout"
                  link={`${routes.logout}`}
                  isPrivate={true}
                  exact={true}
                />
                <PrivateRoute path="/user-details">
                  <UserDetails />
                </PrivateRoute>
                <Route path="/loggedin">
                  <LoggedIn />
                </Route>
                <Route path="/loggedout">
                  <LoggedOut />
                </Route>
              </Switch>
            </div>
          </Router>
        </UserProvider>
      </ApiProvider>
    </CookiesProvider>
  );
}