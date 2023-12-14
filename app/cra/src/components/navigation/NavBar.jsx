import React from "react";
import { Container, Nav, Navbar, NavDropdown } from "react-bootstrap";
import { useUserContext } from "../../contexts/UserContext";
import { RoleEnum } from "../../constants/RoleEnum";
import routes from "../../constants/RouteConstants";

const NswNavBar = () => {
  const { user } = useUserContext();
  const loggedOutView = (
    <Navbar>
      <Container>
        <Navbar.Brand href={routes.frontend.slash}>NSW</Navbar.Brand>
        <Navbar.Collapse id="main-nav">
          <Nav>
            <Nav.Link href={routes.frontend.index}>Index</Nav.Link>
            <Nav.Link href={routes.frontend.about}>About</Nav.Link>
            <Nav.Link href={routes.frontend.search}>Search</Nav.Link>
            <Nav.Link href={routes.frontend.posts}>Posts</Nav.Link>
            <Nav.Link href={routes.frontend.register}>Register</Nav.Link>
            <Nav.Link href={routes.frontend.login}>Login</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );

  const adminMenuItems =
    user.role === RoleEnum.Admin ? (
      <>
        <NavDropdown.Item href={routes.frontend.admin.labelText}>
          Label Text
        </NavDropdown.Item>
        <NavDropdown.Item href={routes.frontend.admin.postCategory}>
          Post Categories
        </NavDropdown.Item>
        <NavDropdown.Item href={routes.frontend.admin.users}>
          Users
        </NavDropdown.Item>
      </>
    ) : (
      <></>
    );

  const loggedInView = (
    <Navbar>
      <Container>
        <Navbar.Brand href={routes.frontend.slash}>NSW</Navbar.Brand>
        <Navbar.Collapse id="main-nav">
          <Nav>
            <Nav.Link href={routes.frontend.index}>Index</Nav.Link>
            <Nav.Link href={routes.frontend.about}>About</Nav.Link>
            <Nav.Link href={routes.frontend.search}>Search</Nav.Link>
            <Nav.Link href={routes.frontend.posts}>Posts</Nav.Link>
            <Nav.Link href={routes.frontend.myPosts}>My Posts</Nav.Link>
            <NavDropdown title="Members" id="account-dropdown">
              <NavDropdown.Item href={routes.frontend.userDetails}>
                Profile
              </NavDropdown.Item>
              {adminMenuItems}
              <NavDropdown.Divider />
              <NavDropdown.Item href={routes.frontend.logout}>
                Log out
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
