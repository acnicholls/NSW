import "../App.css";
import React, { useState } from "react";
import PropTypes from "prop-types";

import {
  Container,
  Row,
  Col,
  Button,
  Modal,
  ModalBody,
  ModalHeader,
} from "react-bootstrap";
import { useUserContext } from "../contexts/UserContext";
import TitleBarComponent from "./TitleBarComponent";
import { useUserInfo } from "../hooks/userHooks";
import { useNavigate } from "react-router-dom";
import routes from "../constants/RouteConstants";
import LabelTextLocators from "../constants/LabelTextLocators";
import { useLabelTextByGroupIdentifier } from "../hooks/labelTextHooks";
import { useLanguageSelectionContext } from "../contexts/LanguageSelectionContext";

const Splash = (props) => {
  //const { navigate } = useNavigate();
  const { selectedLanguage, setSelectedLanguage } =
    useLanguageSelectionContext();
  //   //const { user, setUser } = useUserContext();
  const [labelText, setLabelText] = useState(null);

  var isLabelTextQueryDisabled = labelText !== null;
  const onLabelTextSuccess = (data) => {
    console.log("onSuccess LabelText Splash:", data);
    setLabelText(data.data);
  };

  const onLabelTextError = (error) => {};
  const labelTextLocator = LabelTextLocators.Splash.Main;

  const { data, error, isLoading, isError } = useLabelTextByGroupIdentifier(
    labelTextLocator,
    isLabelTextQueryDisabled,
    onLabelTextSuccess,
    onLabelTextError
  );

  //   /*
  //   Done handling label texts
  // */

  //   var isUserQueryDisabled = false;
  //   const onSuccess = (data) => {
  //     // set the current session user.
  //     //setUser(data);
  //     isUserQueryDisabled = true;
  //     // if session exists, redirect to Loggedin
  //     //navigate(routes.frontend.myPosts);
  //   };
  //   const onError = (error) => {
  //     // log the problem, allow the splash page to show.
  //     console.log("error in splash.jsx", error);
  //   };
  //   // check to see if user has a persistent session on the BFF
  //   const { isLoading, isError } = useUserInfo(
  //     isUserQueryDisabled,
  //     onSuccess,
  //     onError
  //   );

  if (isLoading) {
    return <>Loading...</>;
  }
  // force the Anonymous user to select the language they want displayed
  const setAnonymousUserDisplayLanguage = (selectedLanguage) => {
    setSelectedLanguage(selectedLanguage);
  };

  var showModal = selectedLanguage === 0;

  const defaultReturnValue = (
    <>
      <Button onClick={() => setAnonymousUserDisplayLanguage(2)}>
        {"English 英語"}
      </Button>
      <Button onClick={() => setAnonymousUserDisplayLanguage(1)}>
        {"Japanese 日本語"}
      </Button>
    </>
  );

  var returnValue = defaultReturnValue;
  //   const checkingReturnValue = <>{"Checking for current user session..."}</>;

  //   if (isLoading) {
  //     returnValue = checkingReturnValue;
  //   }

  //   if (isError) {
  //     returnValue = defaultReturnValue;
  //   }
  return (
    <>
      <Container className="splash" show={showModal} fullscreen={true}>
        <Row>
          <Col>
            <Row>
              <Col>{labelText && labelText.Welcome?.japanese}</Col>
            </Row>
            <Row>
              <Col>{labelText && labelText["Welcome"]?.english}</Col>
            </Row>
          </Col>
        </Row>
        <Row>
          <Col>
            <img
              src={`${process.env.PUBLIC_URL}/images/Splash.jpg`}
              alt={"SplashImage"}
            />
          </Col>
        </Row>
        <Row>
          <Col>
            <Row>
              <Col>{labelText && labelText["Instructions"].japanese}</Col>
            </Row>
            <Row>
              <Col>{labelText && labelText["Instructions"].english}</Col>
            </Row>
          </Col>
        </Row>
        <Row>
          <Col>{returnValue}</Col>
        </Row>
      </Container>
    </>
  );
};

export default Splash;

Splash.propTypes = {};
