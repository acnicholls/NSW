import routes from "../constants/RouteConstants";
import * as api from "./api";
const baseRoute = `${routes.backend.postalCode}`;

const getPostalCodes = async () => {
  try {
    var response = await api.apiGet(baseRoute);
    console.log("getPostalCode:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const getPostalCodeById = async (id) => {
  try {
    var response = await api.apiGet(`${baseRoute}/${id}`);
    console.log("getPostalCodeById:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const savePostalCode = async (postalCode) => {
  try {
    var response = await api.apiPostalCode(baseRoute, postalCode);
    console.log("savePostalCode:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const updatePostalCode = async (postalCode) => {
  try {
    var response = await api.apiPut(baseRoute, postalCode);
    console.log("updatePostalCode:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const deletePostalCode = async (postalCode) => {
  try {
    var response = await api.apiDelete(baseRoute, postalCode);
    console.log("deletePostalCode:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
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
