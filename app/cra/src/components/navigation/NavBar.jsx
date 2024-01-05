import React, { useState } from "react";
import { Container, Nav, Navbar, NavDropdown } from "react-bootstrap";
import { NavLink, Navigate } from "react-router-dom";
import { useUserContext } from "../../contexts/UserContext";
import { RoleEnum } from "../../constants/RoleEnum";
import routes from "../../constants/RouteConstants";
import { useLabelTextByGroupIdentifier } from "../../hooks/labelTextHooks";
import LabelTextLocators from "../../constants/LabelTextLocators";
import LabelTextViewComponent from "../LabelText/LabelTextViewComponent";
import getDisplayFromLabelText from "../../functions/getDisplayFromLabelText";

const NswNavBar = () => {
  const { user, selectedLanguage } = useUserContext();
  console.log("user in NavBar", user);
  const [labelText, setLabelText] = useState([]);
  const [isQueryDisabled, setIsQueryDisabled] = useState(false);

  console.log("NswNavBar.labelText", labelText);

  console.log(
    "NswNavBar.labelText item",
    labelText[LabelTextLocators.Master.btnContact]
  );

  const onSuccess = (data) => {
    console.log("success getting label text", data);
    setLabelText(data.data);
    setIsQueryDisabled(true);
  };

  const onError = (error) => {
    console.log("error getting label text", error);
  };

  const { isLoading } = useLabelTextByGroupIdentifier(
    LabelTextLocators.Master.Main,
    isQueryDisabled,
    onSuccess,
    onError
  );

  if (isLoading) {
    return <>Loading...</>;
  }

  const indexNavLink = (
    <NavLink className="nav-link" to={routes.frontend.index}>
      {getDisplayFromLabelText(
        labelText[LabelTextLocators.Master.btnHome],
        user,
        selectedLanguage
      )}
    </NavLink>
  );
  const searchNavLink = (
    <NavLink className="nav-link" to={routes.frontend.search}>
      {getDisplayFromLabelText(
        labelText[LabelTextLocators.Master.btnSearch],
        user,
        selectedLanguage
      )}
    </NavLink>
  );
  const postsNavLink = (
    <NavLink className="nav-link" to={routes.frontend.posts}>
      {getDisplayFromLabelText(
        labelText[LabelTextLocators.Master.btnPosts],
        user,
        selectedLanguage
      )}
    </NavLink>
  );

  const loggedOutView = (
    <Navbar>
      <Container>
        <Navbar.Brand href={routes.frontend.slash}>NSW</Navbar.Brand>
        <Navbar.Collapse id="main-nav">
          <Nav>
            {indexNavLink}
            {searchNavLink}
            {postsNavLink}
            <NavLink className="nav-link" to={routes.frontend.register}>
              {getDisplayFromLabelText(
                labelText[LabelTextLocators.Master.btnRegister],
                user,
                selectedLanguage
              )}
            </NavLink>
            <NavLink className="nav-link" to={routes.frontend.login}>
              {getDisplayFromLabelText(
                labelText[LabelTextLocators.Master.lblLogin],
                user,
                selectedLanguage
              )}
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
            {getDisplayFromLabelText(
              labelText[LabelTextLocators.Master.btlLabel],
              user,
              selectedLanguage
            )}
          </NavLink>
        </NavDropdown.Item>
        <NavDropdown.Item>
          <NavLink className="nav-link" to={routes.frontend.admin.postCategory}>
            {getDisplayFromLabelText(
              labelText[LabelTextLocators.Master.btnCategory],
              user,
              selectedLanguage
            )}
          </NavLink>
        </NavDropdown.Item>
        {/*         <NavDropdown.Item>
          <NavLink className="nav-link" to={routes.frontend.admin.users}>
            <LabelTextViewComponent
              currentLabelText={labelText[LabelTextLocators.Master.btnCategory]}
            />
          </NavLink>
        </NavDropdown.Item> */}
      </>
    ) : (
      <></>
    );

  const loggedInView = (
    <Navbar>
      <Container>
        <Navbar.Brand to={routes.frontend.splash}>NSW</Navbar.Brand>
        <Navbar.Collapse id="main-nav">
          <Nav>
            {indexNavLink}
            {searchNavLink}
            {postsNavLink}
            <NavLink className="nav-link" to={routes.frontend.myPosts}>
              {getDisplayFromLabelText(
                labelText[LabelTextLocators.Master.btnMy],
                user,
                selectedLanguage
              )}
            </NavLink>
            <NavDropdown
              title={getDisplayFromLabelText(
                labelText[LabelTextLocators.Master.btnMember],
                user,
                selectedLanguage
              )}
              id="account-dropdown"
            >
              <NavDropdown.Item>
                <NavLink className="nav-link" to={routes.frontend.userDetails}>
                  {getDisplayFromLabelText(
                    labelText[LabelTextLocators.Master.btnProfile],
                    user,
                    selectedLanguage
                  )}
                </NavLink>
              </NavDropdown.Item>
              {adminMenuItems}
              <NavDropdown.Divider />
              <NavDropdown.Item>
                <NavLink className="nav-link" to={routes.frontend.logout}>
                  {getDisplayFromLabelText(
                    labelText[LabelTextLocators.Master.lblLogout],
                    user,
                    selectedLanguage
                  )}
                </NavLink>
              </NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );

  const userView = user && user.isAuthenticated ? loggedInView : loggedOutView;
  console.log("finished calculating nav bar....");
  return <>{userView}</>;
};

export default NswNavBar;
