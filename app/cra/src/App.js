import "./App.css";

import React from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect,
} from "react-router-dom";
import routes from "./constants/RouteConstants";
/*
  Components
*/
import About from "./components/About";
import Index from "./components/Index";
import Denied from "./components/Denied";
import Posts from "./components/Posts";
import Search from "./components/Search";
import UserDetails from "./components/UserDetails";
import LoggedIn from "./components/authentication/LoggedIn";
import LoggedOut from "./components/authentication/LoggedOut";
import TitleBarComponent from "./components/TitleBarComponent";
import LabelTextComponent from "./components/LabelTextComponent";
/*
  Hook Contexts
*/
import { CookiesProvider } from "react-cookie";
import { UserProvider } from "./contexts/UserContext";
import { ApiProvider } from "./contexts/ApiContext";
import { WeatherProvider } from "./contexts/WeatherContext";
/*
  NavBar components
*/
import NswNavBar from "./components/navigation/NavBar";
import ExternalRedirect from "./components/navigation/ExternalRedirect";
import PrivateRoute from "./components/navigation/PrivateRoute";
import RoleProtectedRoute from "./components/navigation/RoleProtectedRoute";

export default function App() {
  return (
    <>
      <TitleBarComponent />
      <CookiesProvider>
        <ApiProvider>
          <UserProvider>
            <Router>
              <NswNavBar />
              <div>
                <Switch>
                  <Route exact path={routes.frontend.slash}>
                    <Redirect to={routes.frontend.index} />
                  </Route>
                  <Route path={routes.frontend.index}>
                    <Index />
                  </Route>
                  <Route path={routes.frontend.about}>
                    <WeatherProvider>
                      <About />
                    </WeatherProvider>
                  </Route>
                  <Route path={routes.frontend.search}>
                    <Search />
                  </Route>
                  <Route path={routes.frontend.posts}>
                    <Posts />
                  </Route>
                  <PrivateRoute path={routes.frontend.myPosts}>
                    <Posts variant={"My"} />
                  </PrivateRoute>
                  <PrivateRoute path={routes.frontend.userDetails}>
                    <UserDetails />
                  </PrivateRoute>
                  <RoleProtectedRoute
                    path={routes.frontend.admin.labelText}
                    requiredRole={"ADMIN"}
                  >
                    <LabelTextComponent />
                  </RoleProtectedRoute>
                  <RoleProtectedRoute
                    path={routes.frontend.admin.postCategory}
                    requiredRole={"ADMIN"}
                  >
                    <LabelTextComponent />
                  </RoleProtectedRoute>
                  <RoleProtectedRoute
                    path={routes.frontend.admin.users}
                    requiredRole={"ADMIN"}
                  >
                    <LabelTextComponent />
                  </RoleProtectedRoute>
                  <ExternalRedirect
                    path={routes.frontend.register}
                    link={routes.backend.register}
                  />
                  <ExternalRedirect
                    path={routes.frontend.login}
                    link={routes.backend.login}
                    exact={true}
                  />
                  <ExternalRedirect
                    path={routes.frontend.logout}
                    link={routes.backend.logout}
                    isPrivate={true}
                    exact={true}
                  />
                  <Route path={routes.frontend.loggedIn}>
                    <LoggedIn />
                  </Route>
                  <Route path={routes.frontend.loggedOut}>
                    <LoggedOut />
                  </Route>
                  <Route path={routes.frontend.denied}>
                    <Denied />
                  </Route>
                </Switch>
              </div>
            </Router>
          </UserProvider>
        </ApiProvider>
      </CookiesProvider>
    </>
  );
}
