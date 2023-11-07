import React from "react";
import { PropTypes } from "prop-types";
import { useUserContext } from "../contexts/UserContext";
import { usePostCategoryContext } from "../contexts/PostCategoryContext";
import { Row, Col, FormCheck, Button } from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";

const PostCategoryComponent = ({ id }) => {
  const {
    postCategory,
    setPostCategory,
    getPostCategory,
    getPostCategoryById,
    postCategoryList,
    setPostCategoryList,
  } = usePostCategoryContext();
  const { user } = useUserContext();

  return <>the postCategory or postCategory list goes here.</>;
};

export default PostCategoryComponent;

PostCategoryComponent.propTypes = {
  id: PropTypes.number,
};
