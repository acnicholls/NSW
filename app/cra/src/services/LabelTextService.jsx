import routes from "../constants/RouteConstants";
import * as api from "./api";
const baseRoute = `${routes.backend.labelText}`;
const bffGroupRoute = `${routes.backend.publicLabelTextGroup}`;
const apiGroupRoute = `${routes.backend.privateLabelTextGroup}`;

const getLabelTexts = async () => {
  try {
    var response = await api.apiGet(baseRoute);
    console.log("getLabelText:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const getLabelTextById = async (id) => {
  try {
    var response = await api.apiGet(`${baseRoute}/${id}`);
    console.log("getLabelTextById:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const getLabelTextByIdentifier = async (identifier) => {
  try {
    var response = await api.apiGet(`${baseRoute}/${identifier}`);
    console.log("getLabelTextByIdentifier:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const getLabelTextByPageIdentifier = async (identifier) => {
  try {
    var response = await api.apiGet(
      `${routes.backend.privateLabelTextGroup}/${identifier}`
    );
    console.log("getLabelTextByPageIdentifier:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const getAllLabelTextByPageIdentifier = async (identifier) => {
  try {
    var response = await api.apiGet(`${bffGroupRoute}/${identifier}/all`);
    console.log("getAllLabelTextByPageIdentifier:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const saveLabelText = async (labelText) => {
  try {
    var response = await api.apiPost(baseRoute, labelText);
    console.log("saveLabelText:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const updateLabelText = async (labelText) => {
  try {
    var response = await api.apiPut(baseRoute, labelText);
    console.log("updateLabelText:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const deleteLabelText = async (labelText) => {
  try {
    var response = await api.apiDelete(routes.labelText, labelText);
    console.log("deleteLabelText:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const service = {
  deleteLabelText,
  updateLabelText,
  saveLabelText,
  getLabelTextById,
  getLabelTextByIdentifier,
  getLabelTextByPageIdentifier,
  getAllLabelTextByPageIdentifier,
  getLabelTexts,
};

export default service;
