import React from "react";
import { PropTypes } from "prop-types";
import { useUserContext } from "../contexts/UserContext";
import { usePostContext } from "../contexts/PostContext";
import { Row, Col, FormCheck, Button } from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";

const PostComponent = ({ id }) => {
  const { post, setPost, getPost, getPostById, postList, setPostList } =
    usePostContext();
  const { user } = useUserContext();

  return <>the post info goes here.</>;
};

export default PostComponent;

PostComponent.propTypes = {
  id: PropTypes.number,
};
