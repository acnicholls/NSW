import React from "react";
import { useUserContext } from "../../contexts/UserContext";
import { Row, Col } from "react-bootstrap";
import { labelTextShape } from "../../shapes/shapes";

const LabelTextViewComponent = ({ currentLabelText }) => {
  const { user } = useUserContext();

  const labelDisplay =
    user.languagePreference === 2
      ? currentLabelText.japanese
      : currentLabelText.english;

  const viewModeReturnValue = (
    <>
      <Row>
        <Col>{labelDisplay}</Col>
      </Row>
    </>
  );

  return viewModeReturnValue;
};
export default LabelTextViewComponent;

LabelTextViewComponent.propTypes = {
  currentLabelText: labelTextShape,
};
