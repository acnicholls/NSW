import React from "react";
import { PropTypes } from "prop-types";
import { useUserContext } from "../contexts/UserContext";
import { usePostContext } from "../../contexts/PostContext";
import { Row, Col, FormCheck, Button } from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";

const ViewPostList = () => {
  const { getPost, postList, setPostList } = usePostContext();
  const { user } = useUserContext();

  setPostList(getPost());
  return <>the post list goes here.</>;
};

export default ViewPostList;

ViewPostList.propTypes = {};
