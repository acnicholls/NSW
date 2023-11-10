import "./App.css";

import React from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect,
} from "react-router-dom";
import About from "./components/About";
import Index from "./components/Index";
import UserDetails from "./components/UserDetails";
import Posts from "./components/Posts";
import Search from "./components/Search";
import PrivateRoute from "./components/navigation/PrivateRoute";
import NavBar from "./components/navigation/NavBar";
import { CookiesProvider } from "react-cookie";
import { ApiProvider } from "./contexts/ApiContext";
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
                <Route path="/search">
                  <Search />
                </Route>
                <Route path="/posts">
                  <Posts />
                </Route>
                <Route path="/my-posts">
                  <Posts variant={"My"} />
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
