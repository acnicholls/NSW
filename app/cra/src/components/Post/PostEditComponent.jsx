import React from "react";
import { PropTypes } from "prop-types";
import { Container, Row, Col, FormCheck, Button } from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";
/*
Contexts
*/
import { useUserContext } from "../../contexts/UserContext";
/* 
  Components
*/
/*
  Hooks
*/
/* */
/* */
/* */

/*
 - the post component
 - allows the user to view and/or edit a post
*/
const PostComponent = ({ id }) => {
  const { user, selectedLanguage } = useUserContext();

  // define success/error of query

  // get post details from react-query

  // handle loading/errors of query

  // define success/error of mutation

  // define mutation for save/update

  // handle loading/errors of mutation
  return (
    <>
      <Container>
        <Row>
          <Col>post title</Col>
          <Col>post price</Col>
        </Row>
        <Row>
          <Col>post image</Col>
        </Row>
        <Row>
          <Col>post gallery</Col>
        </Row>
        <Row>
          <Col>post description</Col>
        </Row>
        <Row>
          <Col></Col>
          <Col>post user details</Col>
        </Row>
      </Container>
    </>
  );
};

export default PostComponent;

PostComponent.propTypes = {
  id: PropTypes.number,
};
