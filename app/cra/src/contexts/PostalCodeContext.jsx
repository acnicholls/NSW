import React, { createContext, useContext, useState } from "react";
import PropTypes from "prop-types";
import routes from "../constants/RouteConstants";
import { useApiContext } from "./ApiContext";

const PostalCodeContext = createContext(null);

export function usePostalCodeContext() {
  return useContext(PostalCodeContext);
}

export function PostalCodeProvider({ children }) {
  const [postalCode, setPostalCode] = useState(null);
  const [postalCodeList, setPostalCodeList] = useState([]);
  const api = useApiContext();

  const getPostalCode = async () => {
    try {
      var postalCodeInfo = await api.apiGet(routes.postalCode);
      console.log("getPostalCode:response:", postalCodeInfo);
      if (postalCodeInfo && postalCodeInfo.status === 200) {
        setPostalCodeList(postalCodeInfo.data);
      } else {
        setPostalCodeList([]);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const getPostalCodeById = async (id) => {
    try {
      var postalCodeInfo = await api.apiGet(`${routes.postalCode}/${id}`);
      console.log("getPostalCodeById:response:", postalCodeInfo);
      if (postalCodeInfo && postalCodeInfo.status === 200) {
        setPostalCode(postalCodeInfo.data);
      } else {
        setPostalCode(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const savePostalCode = async (postalCode) => {
    try {
      var postalCodeInfo = await api.apiPostalCode(
        routes.postalCode,
        postalCode
      );
      console.log("savePostalCode:response:", postalCodeInfo);
      if (
        postalCodeInfo &&
        postalCodeInfo.status >= 200 &&
        postalCodeInfo.status <= 400
      ) {
        setPostalCode(postalCodeInfo.data);
      } else {
        setPostalCode(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const updatePostalCode = async (postalCode) => {
    try {
      var postalCodeInfo = await api.apiPut(routes.postalCode, postalCode);
      console.log("updatePostalCode:response:", postalCodeInfo);
      if (
        postalCodeInfo &&
        postalCodeInfo.status >= 200 &&
        postalCodeInfo.status <= 400
      ) {
        setPostalCode(postalCodeInfo.data);
      } else {
        setPostalCode(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const deletePostalCode = async (postalCode) => {
    try {
      var postalCodeInfo = await api.apiDelete(routes.postalCode, postalCode);
      console.log("deletePostalCode:response:", postalCodeInfo);
      if (
        postalCodeInfo &&
        postalCodeInfo.status >= 200 &&
        postalCodeInfo.status <= 400
      ) {
        setPostalCode(null);
      } else {
        setPostalCode(null);
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <PostalCodeContext.Provider
      value={{
        postalCode,
        postalCodeList,
        setPostalCode,
        setPostalCodeList,
        savePostalCode,
        updatePostalCode,
        deletePostalCode,
        getPostalCode,
        getPostalCodeById,
      }}
    >
      {children}
    </PostalCodeContext.Provider>
  );
}

PostalCodeProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
