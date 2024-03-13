import routes from "../constants/RouteConstants";
import * as api from "./api";
const bffRoute = `${routes.backend.publicPostCategory}`;
const baseRoute = `${routes.backend.privatePostCategory}`;

const getPostCategories = async () => {
  try {
    var response = await api.apiGet(bffRoute);
    console.log("getPostCategory:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const getPostCategoryById = async (id) => {
  try {
    var response = await api.apiGet(`${baseRoute}/${id}`);
    console.log("getPostCategoryById:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const savePostCategory = async (postCategory) => {
  try {
    var response = await api.apiPost(baseRoute, postCategory);
    console.log("savePostCategory:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const updatePostCategory = async (postCategory) => {
  try {
    var response = await api.apiPut(baseRoute, postCategory);
    console.log("updatePostCategory:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const deletePostCategory = async (postCategory) => {
  try {
    var response = await api.apiDelete(baseRoute, postCategory);
    console.log("deletePostCategory:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

var service = {
  deletePostCategory,
  updatePostCategory,
  savePostCategory,
  getPostCategoryById,
  getPostCategories,
};

export default service;
