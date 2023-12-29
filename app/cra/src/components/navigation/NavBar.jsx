import React from "react";
import { Container, Nav, Navbar, NavDropdown } from "react-bootstrap";
import { NavLink } from "react-router-dom";
import { useUserContext } from "../../contexts/UserContext";
import { RoleEnum } from "../../constants/RoleEnum";
import routes from "../../constants/RouteConstants";

const NswNavBar = () => {
  const { user } = useUserContext();
  console.log("user in NavBar", user);
  const loggedOutView = (
    <Navbar>
      <Container>
        <Navbar.Brand href={routes.frontend.slash}>NSW</Navbar.Brand>
        <Navbar.Collapse id="main-nav">
          <Nav>
            <NavLink className="nav-link" to={routes.frontend.index}>
              Index
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.about}>
              About
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.search}>
              Search
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.posts}>
              Posts
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.register}>
              Register
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.login}>
              Login
            </NavLink>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );

  const adminMenuItems =
    user.role === RoleEnum.Admin ? (
      <>
        <NavDropdown.Item>
          <NavLink className="nav-link" to={routes.frontend.admin.labelText}>
            Label Text
          </NavLink>
        </NavDropdown.Item>
        <NavDropdown.Item>
          <NavLink className="nav-link" to={routes.frontend.admin.postCategory}>
            Post Categories
          </NavLink>
        </NavDropdown.Item>
        <NavDropdown.Item>
          <NavLink className="nav-link" to={routes.frontend.admin.users}>
            Users
          </NavLink>
        </NavDropdown.Item>
      </>
    ) : (
      <></>
    );

  const loggedInView = (
    <Navbar>
      <Container>
        <Navbar.Brand to={routes.frontend.slash}>NSW</Navbar.Brand>
        <Navbar.Collapse id="main-nav">
          <Nav>
            <NavLink className="nav-link" to={routes.frontend.index}>
              Index
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.about}>
              About
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.search}>
              Search
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.posts}>
              Posts
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.myPosts}>
              My Posts
            </NavLink>
            <NavDropdown title="Members" id="account-dropdown">
              <NavDropdown.Item>
                <NavLink className="nav-link" to={routes.frontend.userDetails}>
                  Profile
                </NavLink>
              </NavDropdown.Item>
              {adminMenuItems}
              <NavDropdown.Divider />
              <NavDropdown.Item>
                <NavLink className="nav-link" to={routes.frontend.logout}>
                  Log out
                </NavLink>
              </NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );

  const userView = user && user.isAuthenticated ? loggedInView : loggedOutView;
  return <>{userView}</>;
};

export default NswNavBar;
