import "./App.css";

import React from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import routes from "./constants/RouteConstants";
/*
  Components
*/
import About from "./components/About";
import Index from "./components/Index";
import Denied from "./components/Denied";
import Posts from "./components/Post/Posts";
import Search from "./components/Search";
import UserDetails from "./components/UserDetails";
import LoggedIn from "./components/authentication/LoggedIn";
import LoggedOut from "./components/authentication/LoggedOut";
import TitleBarComponent from "./components/TitleBarComponent";
import LabelTextEditComponent from "./components/LabelText/LabelTextEditComponent";
import NotFound from "./components/NotFound";
/*
  Hook Contexts
*/
import { UserProvider } from "./contexts/UserContext";
import { QueryClient, QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
/*
  NavBar components
*/
import NswNavBar from "./components/navigation/NavBar";
import ExternalRedirect from "./components/navigation/ExternalRedirect";
import RequireAuth from "./components/authentication/RequireAuth";
import RequireRole from "./components/authentication/RequireRole";
import { RoleEnum } from "./constants/RoleEnum";

const queryClient = new QueryClient();

export default function App() {
  return (
    <>
      <QueryClientProvider client={queryClient}>
        <TitleBarComponent />
        <UserProvider>
          <Router>
            <NswNavBar />
            <div>
              <Routes>
                <Route
                  exact
                  path={routes.frontend.slash}
                  element={<Navigate to={routes.frontend.index} />}
                />
                <Route path={routes.frontend.index} element={<Index />} />
                <Route path={routes.frontend.about} element={<About />} />
                <Route path={routes.frontend.search} element={<Search />} />
                <Route path={routes.frontend.posts} element={<Posts />} />
                <Route
                  path={routes.frontend.myPosts}
                  element={
                    <RequireAuth>
                      <Posts variant={"My"} />
                    </RequireAuth>
                  }
                />
                <Route
                  path={routes.frontend.userDetails}
                  element={
                    <RequireAuth>
                      <UserDetails />
                    </RequireAuth>
                  }
                />
                <Route
                  path={routes.frontend.admin.labelText}
                  element={
                    <RequireRole selectedRole={RoleEnum.Admin}>
                      <LabelTextEditComponent />
                    </RequireRole>
                  }
                />
                <Route
                  path={routes.frontend.admin.postCategory}
                  requiredRole={"ADMIN"}
                  element={
                    <RequireRole selectedRole={RoleEnum.Admin}>
                      <LabelTextEditComponent />
                    </RequireRole>
                  }
                />
                <Route
                  path={routes.frontend.admin.users}
                  requiredRole={"ADMIN"}
                  element={
                    <RequireRole selectedRole={RoleEnum.Admin}>
                      <LabelTextEditComponent />
                    </RequireRole>
                  }
                />
                <Route
                  path={routes.frontend.register}
                  element={
                    <ExternalRedirect
                      path={routes.frontend.register}
                      link={routes.backend.register}
                    />
                  }
                />
                <Route
                  path={routes.frontend.login}
                  element={
                    <ExternalRedirect
                      path={routes.frontend.login}
                      link={routes.backend.login}
                      exact={true}
                    />
                  }
                />
                <Route
                  path={routes.frontend.logout}
                  element={
                    <ExternalRedirect
                      path={routes.frontend.logout}
                      link={routes.backend.logout}
                      isPrivate={true}
                      exact={true}
                    />
                  }
                />
                <Route path={routes.frontend.loggedIn} element={<LoggedIn />} />

                <Route
                  path={routes.frontend.loggedOut}
                  element={<LoggedOut />}
                />

                <Route path={routes.frontend.denied} element={<Denied />} />
                <Route path="*" element={<NotFound />} />
              </Routes>
            </div>
          </Router>
        </UserProvider>
        <ReactQueryDevtools initialIsOpen={false} />
      </QueryClientProvider>
    </>
  );
}
