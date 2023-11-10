import React from "react";
import { Row, Col } from "react-bootstrap";
import PropTypes from "prop-types";

const Posts = ({ variant }) => {
  const message =
    variant === "My" ? "this is the MY posts page" : "this is the posts page";

  return (
    <>
      <Row>
        <Col></Col>
        <Col>{message}</Col>
        <Col></Col>
      </Row>
    </>
  );
};

export default Posts;

Posts.propTypes = {
  variant: PropTypes.string,
};
