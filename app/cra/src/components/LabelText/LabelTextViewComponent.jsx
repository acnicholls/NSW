import React, { useState } from "react";
import { useUserContext } from "../../contexts/UserContext";
import { labelTextViewShape } from "../../shapes/shapes";
import getDisplayFromLabelText from "../../functions/getDisplayFromLabelText";

const LabelTextViewComponent = ({ currentLabelText }) => {
  const { user, selectedLanguage } = useUserContext();

  console.log("LabelTextViewComponent.currentLabelText: ", currentLabelText);

  const [displayValue] = useState(
    getDisplayFromLabelText(currentLabelText, user, selectedLanguage)
  );
  console.log("LabelTextViewComponent.displayValue: ", displayValue);
  return <>{displayValue}</>;
};
export default LabelTextViewComponent;

LabelTextViewComponent.propTypes = {
  currentLabelText: labelTextViewShape,
};
