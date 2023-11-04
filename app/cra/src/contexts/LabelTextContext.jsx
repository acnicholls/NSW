import React, { createContext, useContext, useState } from "react";
import PropTypes from "prop-types";
import routes from "../constants/RouteConstants";
import { useApiContext } from "./ApiContext";

const LabelTextContext = createContext(null);

export function useLabelTextContext() {
  return useContext(LabelTextContext);
}

export function LabelTextProvider({ children }) {
  const [labelText, setLabelText] = useState(null);
  const [labelTextList, setLabelTextList] = useState([]);
  const api = useApiContext();

  const getLabelText = async () => {
    try {
      var labelTextInfo = await api.apiGet(routes.labelText);
      console.log("getLabelText:response:", labelTextInfo);
      if (labelTextInfo && labelTextInfo.status === 200) {
        setLabelTextList(labelTextInfo.data);
      } else {
        setLabelTextList([]);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const getLabelTextById = async (id) => {
    try {
      var labelTextInfo = await api.apiGet(`${routes.labelText}/${id}`);
      console.log("getLabelTextById:response:", labelTextInfo);
      if (labelTextInfo && labelTextInfo.status === 200) {
        setLabelText(labelTextInfo.data);
      } else {
        setLabelText(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const saveLabelText = async (labelText) => {
    try {
      var labelTextInfo = await api.apiLabelText(routes.labelText, labelText);
      console.log("saveLabelText:response:", labelTextInfo);
      if (
        labelTextInfo &&
        labelTextInfo.status >= 200 &&
        labelTextInfo.status <= 400
      ) {
        setLabelText(labelTextInfo.data);
      } else {
        setLabelText(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const updateLabelText = async (labelText) => {
    try {
      var labelTextInfo = await api.apiPut(routes.labelText, labelText);
      console.log("updateLabelText:response:", labelTextInfo);
      if (
        labelTextInfo &&
        labelTextInfo.status >= 200 &&
        labelTextInfo.status <= 400
      ) {
        setLabelText(labelTextInfo.data);
      } else {
        setLabelText(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const deleteLabelText = async (labelText) => {
    try {
      var labelTextInfo = await api.apiDelete(routes.labelText, labelText);
      console.log("deleteLabelText:response:", labelTextInfo);
      if (
        labelTextInfo &&
        labelTextInfo.status >= 200 &&
        labelTextInfo.status <= 400
      ) {
        setLabelText(null);
      } else {
        setLabelText(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <LabelTextContext.Provider
      value={{
        labelText,
        labelTextList,
        setLabelText,
        setLabelTextList,
        saveLabelText,
        updateLabelText,
        deleteLabelText,
        getLabelText,
        getLabelTextById,
      }}
    >
      {children}
    </LabelTextContext.Provider>
  );
}

LabelTextProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
