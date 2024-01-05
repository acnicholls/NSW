import React, { useState } from "react";
import { useUserContext } from "../../contexts/UserContext";
import { Row, Col } from "react-bootstrap";
import { labelTextViewShape } from "../../shapes/shapes";
import { LanguagePreference } from "../../constants/LanguagePreference";
import decideDisplayLanguage from "../../functions/decideDisplayLanguage";
import getDisplayFromLabelText from "../../functions/getDisplayFromLabelText";

const LabelTextViewComponent = ({ currentLabelText }) => {
  const { user, selectedLanguage } = useUserContext();

  console.log("LabelTextViewComponent.currentLabelText: ", currentLabelText);

  const labelDisplay = getDisplayFromLabelText(
    currentLabelText,
    user,
    selectedLanguage
  );

  const [displayValue] = useState(labelDisplay);
  const viewModeReturnValue = <>{displayValue}</>;

  return viewModeReturnValue;
};
export default LabelTextViewComponent;

LabelTextViewComponent.propTypes = {
  currentLabelText: labelTextViewShape,
};
