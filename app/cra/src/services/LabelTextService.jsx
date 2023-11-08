import routes from "../constants/RouteConstants";
import * as api from "./api";
const baseRoute = `${routes.labelText}`;

const getLabelTexts = async () => {
  try {
    var labelTextInfo = await api.apiGet(baseRoute);
    console.log("getLabelText:response:", labelTextInfo);
    if (labelTextInfo && labelTextInfo.status === 200) {
      return labelTextInfo.data;
    } else {
      return labelTextInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const getLabelTextById = async (id) => {
  try {
    var labelTextInfo = await api.apiGet(`${baseRoute}/${id}`);
    console.log("getLabelTextById:response:", labelTextInfo);
    if (labelTextInfo && labelTextInfo.status === 200) {
      return labelTextInfo.data;
    } else {
      return labelTextInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const saveLabelText = async (labelText) => {
  try {
    var labelTextInfo = await api.apiPost(baseRoute, labelText);
    console.log("saveLabelText:response:", labelTextInfo);
    if (
      labelTextInfo &&
      labelTextInfo.status >= 200 &&
      labelTextInfo.status <= 400
    ) {
      return labelTextInfo.data;
    } else {
      return labelTextInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const updateLabelText = async (labelText) => {
  try {
    var labelTextInfo = await api.apiPut(baseRoute, labelText);
    console.log("updateLabelText:response:", labelTextInfo);
    if (
      labelTextInfo &&
      labelTextInfo.status >= 200 &&
      labelTextInfo.status <= 400
    ) {
      return labelTextInfo.data;
    } else {
      return labelTextInfo.error;
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
      return labelTextInfo.data;
    } else {
      return labelTextInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const service = {
  deleteLabelText,
  updateLabelText,
  saveLabelText,
  getLabelTextById,
  getLabelTexts,
};

export default service;
