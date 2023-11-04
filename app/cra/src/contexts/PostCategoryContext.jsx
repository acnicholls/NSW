import React, { createContext, useContext, useState } from "react";
import PropTypes from "prop-types";
import routes from "../constants/RouteConstants";
import { useApiContext } from "./ApiContext";

const PostCategoryContext = createContext(null);

export function usePostCategoryContext() {
  return useContext(PostCategoryContext);
}

export function PostCategoryProvider({ children }) {
  const [postCategory, setPostCategory] = useState(null);
  const [postCategoryList, setPostCategoryList] = useState([]);
  const api = useApiContext();

  const getPostCategory = async () => {
    try {
      var postCategoryInfo = await api.apiGet(routes.postCategory);
      console.log("getPostCategory:response:", postCategoryInfo);
      if (postCategoryInfo && postCategoryInfo.status === 200) {
        setPostCategoryList(postCategoryInfo.data);
      } else {
        setPostCategoryList([]);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const getPostCategoryById = async (id) => {
    try {
      var postCategoryInfo = await api.apiGet(`${routes.postCategory}/${id}`);
      console.log("getPostCategoryById:response:", postCategoryInfo);
      if (postCategoryInfo && postCategoryInfo.status === 200) {
        setPostCategory(postCategoryInfo.data);
      } else {
        setPostCategory(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const savePostCategory = async (postCategory) => {
    try {
      var postCategoryInfo = await api.apiPostCategory(
        routes.postCategory,
        postCategory
      );
      console.log("savePostCategory:response:", postCategoryInfo);
      if (
        postCategoryInfo &&
        postCategoryInfo.status >= 200 &&
        postCategoryInfo.status <= 400
      ) {
        setPostCategory(postCategoryInfo.data);
      } else {
        setPostCategory(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const updatePostCategory = async (postCategory) => {
    try {
      var postCategoryInfo = await api.apiPut(
        routes.postCategory,
        postCategory
      );
      console.log("updatePostCategory:response:", postCategoryInfo);
      if (
        postCategoryInfo &&
        postCategoryInfo.status >= 200 &&
        postCategoryInfo.status <= 400
      ) {
        setPostCategory(postCategoryInfo.data);
      } else {
        setPostCategory(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const deletePostCategory = async (postCategory) => {
    try {
      var postCategoryInfo = await api.apiDelete(
        routes.postCategory,
        postCategory
      );
      console.log("deletePostCategory:response:", postCategoryInfo);
      if (
        postCategoryInfo &&
        postCategoryInfo.status >= 200 &&
        postCategoryInfo.status <= 400
      ) {
        setPostCategory(null);
      } else {
        setPostCategory(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <PostCategoryContext.Provider
      value={{
        postCategory,
        postCategoryList,
        setPostCategory,
        setPostCategoryList,
        savePostCategory,
        updatePostCategory,
        deletePostCategory,
        getPostCategory,
        getPostCategoryById,
      }}
    >
      {children}
    </PostCategoryContext.Provider>
  );
}

PostCategoryProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
