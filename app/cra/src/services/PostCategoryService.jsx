import routes from "../constants/RouteConstants";
import * as api from "./api";
const baseRoute = `${routes.backend.postCategory}`;

const getPostCategories = async () => {
  try {
    var postCategoryInfo = await api.apiGet(baseRoute);
    console.log("getPostCategory:response:", postCategoryInfo);
    if (postCategoryInfo && postCategoryInfo.status === 200) {
      return postCategoryInfo.data;
    } else {
      return postCategoryInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const getPostCategoryById = async (id) => {
  try {
    var postCategoryInfo = await api.apiGet(`${baseRoute}/${id}`);
    console.log("getPostCategoryById:response:", postCategoryInfo);
    if (postCategoryInfo && postCategoryInfo.status === 200) {
      return postCategoryInfo.data;
    } else {
      return postCategoryInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const savePostCategory = async (postCategory) => {
  try {
    var postCategoryInfo = await api.apiPostCategory(baseRoute, postCategory);
    console.log("savePostCategory:response:", postCategoryInfo);
    if (
      postCategoryInfo &&
      postCategoryInfo.status >= 200 &&
      postCategoryInfo.status <= 400
    ) {
      return postCategoryInfo.data;
    } else {
      return postCategoryInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const updatePostCategory = async (postCategory) => {
  try {
    var postCategoryInfo = await api.apiPut(baseRoute, postCategory);
    console.log("updatePostCategory:response:", postCategoryInfo);
    if (
      postCategoryInfo &&
      postCategoryInfo.status >= 200 &&
      postCategoryInfo.status <= 400
    ) {
      return postCategoryInfo.data;
    } else {
      return postCategoryInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const deletePostCategory = async (postCategory) => {
  try {
    var postCategoryInfo = await api.apiDelete(baseRoute, postCategory);
    console.log("deletePostCategory:response:", postCategoryInfo);
    if (
      postCategoryInfo &&
      postCategoryInfo.status >= 200 &&
      postCategoryInfo.status <= 400
    ) {
      return postCategoryInfo.data;
    } else {
      return postCategoryInfo.error;
    }
  } catch (error) {
    console.log(error);
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
