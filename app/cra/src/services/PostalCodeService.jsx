import routes from "../constants/RouteConstants";
import * as api from "./api";
const baseRoute = `${routes.backend.postalCode}`;

const getPostalCodes = async () => {
  try {
    var postalCodeInfo = await api.apiGet(baseRoute);
    console.log("getPostalCode:response:", postalCodeInfo);
    if (postalCodeInfo && postalCodeInfo.status === 200) {
      return postalCodeInfo.data;
    } else {
      return postalCodeInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const getPostalCodeById = async (id) => {
  try {
    var postalCodeInfo = await api.apiGet(`${baseRoute}/${id}`);
    console.log("getPostalCodeById:response:", postalCodeInfo);
    if (postalCodeInfo && postalCodeInfo.status === 200) {
      return postalCodeInfo.data;
    } else {
      return postalCodeInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const savePostalCode = async (postalCode) => {
  try {
    var postalCodeInfo = await api.apiPostalCode(baseRoute, postalCode);
    console.log("savePostalCode:response:", postalCodeInfo);
    if (
      postalCodeInfo &&
      postalCodeInfo.status >= 200 &&
      postalCodeInfo.status <= 400
    ) {
      return postalCodeInfo.data;
    } else {
      return postalCodeInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const updatePostalCode = async (postalCode) => {
  try {
    var postalCodeInfo = await api.apiPut(baseRoute, postalCode);
    console.log("updatePostalCode:response:", postalCodeInfo);
    if (
      postalCodeInfo &&
      postalCodeInfo.status >= 200 &&
      postalCodeInfo.status <= 400
    ) {
      return postalCodeInfo.data;
    } else {
      return postalCodeInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const deletePostalCode = async (postalCode) => {
  try {
    var postalCodeInfo = await api.apiDelete(baseRoute, postalCode);
    console.log("deletePostalCode:response:", postalCodeInfo);
    if (
      postalCodeInfo &&
      postalCodeInfo.status >= 200 &&
      postalCodeInfo.status <= 400
    ) {
      return postalCodeInfo.data;
    } else {
      return postalCodeInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

var service = {
  deletePostalCode,
  updatePostalCode,
  savePostalCode,
  getPostalCodeById,
  getPostalCode: getPostalCodes,
};

export default service;
