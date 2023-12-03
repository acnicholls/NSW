import React from "react";
import { Container, Nav, Navbar, NavDropdown } from "react-bootstrap";
import { useUserContext } from "../../contexts/UserContext";
import { RoleEnum } from "../../constants/RoleEnum";

const NswNavBar = () => {
  const { user } = useUserContext();
  const loggedOutView = (
    <Navbar>
      <Container>
        <Navbar.Brand href="/">NSW</Navbar.Brand>
        <Navbar.Collapse id="main-nav">
          <Nav>
            <Nav.Link href="/index">Index</Nav.Link>
            <Nav.Link href="/about">About</Nav.Link>
            <Nav.Link href="/search">Search</Nav.Link>
            <Nav.Link href="/posts">Posts</Nav.Link>
            <Nav.Link href="/register">Register</Nav.Link>
            <Nav.Link href="/login">Login</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );

  const adminMenuItems =
    user.role === RoleEnum.Admin ? (
      <>
        <NavDropdown.Item href="admin/label-text">Label Text</NavDropdown.Item>
        <NavDropdown.Item href="admin/post-category">
          Post Categories
        </NavDropdown.Item>
        <NavDropdown.Item href="admin/users">Users</NavDropdown.Item>
      </>
    ) : (
      <></>
    );

  const loggedInView = (
    <Navbar>
      <Container>
        <Navbar.Brand href="/">NSW</Navbar.Brand>
        <Navbar.Collapse id="main-nav">
          <Nav>
            <Nav.Link href="/index">Index</Nav.Link>
            <Nav.Link href="/about">About</Nav.Link>
            <Nav.Link href="/search">Search</Nav.Link>
            <Nav.Link href="/posts">Posts</Nav.Link>
            <Nav.Link href="/my-posts">My Posts</Nav.Link>
            <NavDropdown title="Members" id="account-dropdown">
              <NavDropdown.Item href="/user-details">Profile</NavDropdown.Item>
              {adminMenuItems}
              <NavDropdown.Divider />
              <NavDropdown.Item href="/logout">Log out</NavDropdown.Item>
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
