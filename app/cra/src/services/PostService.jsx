import routes from "../constants/RouteConstants";
import * as api from "./api";
const baseRoute = `${routes.post}`;

const getPosts = async () => {
  try {
    var postInfo = await api.apiGet(baseRoute);
    console.log("getPosts:response:", postInfo);
    if (postInfo && postInfo.status === 200) {
      return postInfo.data;
    } else {
      return postInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const getPostsByCategoryId = async (categoryId) => {
  try {
    var postInfo = await api.apiGet(`${baseRoute}/category/${categoryId}`);
    console.log("getPostsByCategoryId:response:", postInfo);
    if (postInfo && postInfo.status === 200) {
      return postInfo.data;
    } else {
      return postInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const getPostById = async (id) => {
  try {
    var postInfo = await api.apiGet(`${baseRoute}/${id}`);
    console.log("getPostById:response:", postInfo);
    if (postInfo && postInfo.status === 200) {
      return postInfo.data;
    } else {
      return postInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const savePost = async (post) => {
  try {
    var postInfo = await api.apiPost(baseRoute, post);
    console.log("savePost:response:", postInfo);
    if (postInfo && postInfo.status >= 200 && postInfo.status <= 400) {
      return postInfo.data;
    } else {
      return postInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const updatePost = async (post) => {
  try {
    var postInfo = await api.apiPut(baseRoute, post);
    console.log("updatePost:response:", postInfo);
    if (postInfo && postInfo.status >= 200 && postInfo.status <= 400) {
      return postInfo.data;
    } else {
      return postInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

const deletePost = async (post) => {
  try {
    var postInfo = await api.apiDelete(baseRoute, post);
    console.log("deletePost:response:", postInfo);
    if (postInfo && postInfo.status >= 200 && postInfo.status <= 400) {
      return postInfo.data;
    } else {
      return postInfo.error;
    }
  } catch (error) {
    console.log(error);
  }
};

var service = {
  deletePost,
  updatePost,
  savePost,
  getPostById,
  getPost: getPosts,
};

export default service;
