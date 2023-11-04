import React, { createContext, useContext, useState } from "react";
import PropTypes from "prop-types";
import routes from "../constants/RouteConstants";
import { useApiContext } from "./ApiContext";

const PostContext = createContext(null);

export function usePostContext() {
  return useContext(PostContext);
}

export function PostProvider({ children }) {
  const [post, setPost] = useState(null);
  const [postList, setPostList] = useState([]);
  const api = useApiContext();

  const getPost = async () => {
    try {
      var postInfo = await api.apiGet(routes.post);
      console.log("getPost:response:", postInfo);
      if (postInfo && postInfo.status === 200) {
        setPostList(postInfo.data);
      } else {
        setPostList([]);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const getPostById = async (id) => {
    try {
      var postInfo = await api.apiGet(`${routes.post}/${id}`);
      console.log("getPostById:response:", postInfo);
      if (postInfo && postInfo.status === 200) {
        setPost(postInfo.data);
      } else {
        setPost(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const savePost = async (post) => {
    try {
      var postInfo = await api.apiPost(routes.post, post);
      console.log("savePost:response:", postInfo);
      if (postInfo && postInfo.status >= 200 && postInfo.status <= 400) {
        setPost(postInfo.data);
      } else {
        setPost(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const updatePost = async (post) => {
    try {
      var postInfo = await api.apiPut(routes.post, post);
      console.log("updatePost:response:", postInfo);
      if (postInfo && postInfo.status >= 200 && postInfo.status <= 400) {
        setPost(postInfo.data);
      } else {
        setPost(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const deletePost = async (post) => {
    try {
      var postInfo = await api.apiDelete(routes.post, post);
      console.log("deletePost:response:", postInfo);
      if (postInfo && postInfo.status >= 200 && postInfo.status <= 400) {
        setPost(null);
      } else {
        setPost(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <PostContext.Provider
      value={{
        post,
        postList,
        setPost,
        setPostList,
        savePost,
        updatePost,
        deletePost,
        getPost,
        getPostById,
      }}
    >
      {children}
    </PostContext.Provider>
  );
}

PostProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
