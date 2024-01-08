/*
 CSS
*/
import "../App.css";
/*
  React Stuff
*/
import React, { useState } from "react";
import PropTypes from "prop-types";
/*
  Layout
*/
import { Container, Row, Col, Button } from "react-bootstrap";
import { useNavigate } from "react-router";
/*
  Components
*/
import SpacerRow from "./SpacerRow";
/*
  Constants
*/
import LabelTextLocators from "../constants/LabelTextLocators";
/*
  Hooks
*/
import { useLabelTextByGroupIdentifier } from "../hooks/labelTextHooks";
import { useUserContext } from "../contexts/UserContext";
import routes from "../constants/RouteConstants";

const Splash = (props) => {
  const { selectedLanguage, setSelectedLanguage } = useUserContext();
  const [labelText, setLabelText] = useState(null);
  const navigate = useNavigate();

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

  if (isLoading) {
    return <>Loading...</>;
  }
  // force the Anonymous user to select the language they want displayed
  const setAnonymousUserDisplayLanguage = (selectedLanguage) => {
    setSelectedLanguage(selectedLanguage);
    navigate(routes.frontend.index);
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

  return (
    <>
      <Container className="splash">
        <SpacerRow />
        <Row>
          <Col>
            <Row>
              <Col>
                <h1>
                  {labelText &&
                    labelText[LabelTextLocators.Splash.Welcome]?.japanese}
                </h1>
              </Col>
            </Row>
            <Row>
              <Col>
                <h1>
                  {labelText &&
                    labelText[LabelTextLocators.Splash.Welcome]?.english}
                </h1>
              </Col>
            </Row>
          </Col>
        </Row>
        <SpacerRow />
        <Row>
          <Col className="splashImage">
            <img
              className="splashImage"
              src={`${process.env.PUBLIC_URL}/images/Splash.jpg`}
              alt={"SplashImage"}
            />
          </Col>
        </Row>
        <SpacerRow />
        <Row>
          <Col>
            <Row>
              <Col>
                {labelText &&
                  labelText[LabelTextLocators.Splash.Instructions].japanese}
              </Col>
            </Row>
            <Row>
              <Col>
                {labelText &&
                  labelText[LabelTextLocators.Splash.Instructions].english}
              </Col>
            </Row>
          </Col>
        </Row>
        <SpacerRow />
        <Row>
          <Col>{returnValue}</Col>
        </Row>
      </Container>
    </>
  );
};

export default Splash;

Splash.propTypes = {};
