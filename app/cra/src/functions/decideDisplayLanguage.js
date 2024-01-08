import React from "react";
import { LanguagePreference } from "../constants/LanguagePreference";

function decideDisplayLanguage(user, selectedLanguage) {
  // figure out which language to display
  var displayLanguage = LanguagePreference.default;
  // if the user is null, and selectedLanguage is zero, set to default
  // if there is a user, use the user's set pref
  if (user.id > 0) {
    displayLanguage = user.languagePreference;
    // if the user has tried to change their display language, let them
    if (displayLanguage !== selectedLanguage) {
      displayLanguage = selectedLanguage;
    }
  }
  // if there is no user, use the selectedLanguage, if it is not zero
  if (user.id < 0 && selectedLanguage !== 0) {
    displayLanguage = selectedLanguage;
  }
  return displayLanguage;
}

export default decideDisplayLanguage;
