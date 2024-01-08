import React from "react";
import { LanguagePreference } from "../constants/LanguagePreference";
import decideDisplayLanguage from "./decideDisplayLanguage";

function getDisplayFromLabelText(labelText, user, selectedLanguage) {
  console.log("for label text: ", labelText);
  var displayLanguage = decideDisplayLanguage(user, selectedLanguage);
  console.log("decided upon display language: ", displayLanguage);

  if (labelText !== undefined) {
    const labelDisplay =
      displayLanguage === LanguagePreference.default
        ? labelText.japanese
        : labelText.english;
    console.log("returning: ", labelDisplay);
    return labelDisplay;
  }
  console.log("labelText was undefined");
  return "";
}

export default getDisplayFromLabelText;
