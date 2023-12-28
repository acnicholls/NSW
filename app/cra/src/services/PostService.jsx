import routes from "../constants/RouteConstants";
import * as api from "./api";
const baseRoute = `${routes.backend.post}`;

const getPosts = async () => {
  try {
    var response = await api.apiGet(baseRoute);
    console.log("getPosts:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const getPostsByCategoryId = async (categoryId) => {
  try {
    var response = await api.apiGet(`${baseRoute}/category/${categoryId}`);
    console.log("getPostsByCategoryId:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const getPostById = async (id) => {
  try {
    var response = await api.apiGet(`${baseRoute}/${id}`);
    console.log("getPostById:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const savePost = async (post) => {
  try {
    var response = await api.apiPost(baseRoute, post);
    console.log("savePost:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const updatePost = async (post) => {
  try {
    var response = await api.apiPut(baseRoute, post);
    console.log("updatePost:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

const deletePost = async (post) => {
  try {
    var response = await api.apiDelete(baseRoute, post);
    console.log("deletePost:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

var service = {
  deletePost,
  updatePost,
  savePost,
  getPostById,
  getPosts,
  getPostsByCategoryId,
};

export default service;
