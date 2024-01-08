import React, { createContext, useContext, useState } from "react";
import PropTypes from "prop-types";
import Splash from "../components/Splash";

const LanguageSelectionContext = createContext(null);

export function useLanguageSelectionContext() {
  return useContext(LanguageSelectionContext);
}

export function LanguageSelectionProvider({ children }) {
  const [selectedLanguage, setSelectedLanguage] = useState(0);

  var returnValue = children;

  if (selectedLanguage === 0) {
    returnValue = (
      <>
        <Splash />
      </>
    );
  }

  return (
    <LanguageSelectionContext.Provider
      value={{ selectedLanguage, setSelectedLanguage }}
    >
      {returnValue}
    </LanguageSelectionContext.Provider>
  );
}

LanguageSelectionProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
