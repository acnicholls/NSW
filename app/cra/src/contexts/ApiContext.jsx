import React, { useContext, createContext } from "react";
import axios from "axios";

const apiContext = createContext(null);

export function useApiContext() {
  return useContext(apiContext);
}

export function ApiProvider({ children }) {
  axios.defaults.withCredentials = true;

  const apiGet = async (url) => {
    try {
      return await axios.get(url);
    } catch (error) {
      return error.response;
    }
  };

  const apiPost = async (url, data) => {
    try {
      return await axios.post(url, data);
    } catch (error) {
      return error.response;
    }
  };

  const apiPut = async (url, data) => {
    try {
      return await axios.put(url, data);
    } catch (error) {
      return error.response;
    }
  };

  const apiDelete = async (url, data) => {
    try {
      return await axios.delete(url, data);
    } catch (error) {
      return error.response;
    }
  };

  return (
    <apiContext.Provider
      value={{
        apiGet,
        apiPost,
        apiPut,
        apiDelete,
      }}
    >
      {children}
    </apiContext.Provider>
  );
}
