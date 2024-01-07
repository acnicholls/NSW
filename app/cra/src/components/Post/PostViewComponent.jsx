import React, { useState } from "react";
import { PropTypes } from "prop-types";
/* 
  Layout
*/
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
import { usePostInfo } from "../../hooks/postHooks";
import { useParams } from "react-router";
/* */
/* */

/*
 - the post view component
 - allows the user to view a post
*/
const PostViewComponent = () => {
  let params = useParams();
  console.log("inside Post View");
  console.log("params: ", JSON.stringify(params));
  const { user, selectedLanguage } = useUserContext();
  const [post, setPost] = useState(null);

  // define success/error of query
  var queryIsDisabled = false;
  const onSuccess = (data) => {
    console.log("PostViewComponent.OnSuccess", data);
    setPost(data.data);
    queryIsDisabled = true;
  };

  const onError = (error) => {
    console.log("PostViewComponent.onError", error);
  };

  // get post details from react-query
  const { error, isLoading, isError } = usePostInfo(
    params?.id,
    queryIsDisabled,
    onSuccess,
    onError
  );

  // handle loading/errors of query
  if (isLoading) {
    return <>Loading...</>;
  }

  if (isError) {
    return (
      <>
        <Container>
          <Row>
            <Col>Error: {error}</Col>
          </Row>
        </Container>
      </>
    );
  }

  return (
    <>
      <Container>
        <Row>
          <Col className="itemTitle">{post.title}</Col>
          <Col className="itemPrice">{post.price}</Col>
        </Row>
        <Row>
          <Col>post image</Col>
        </Row>
        <Row>
          <Col>post gallery</Col>
        </Row>
        <Row>
          <Col className="itemInfo">{post.description}</Col>
        </Row>
        <Row>
          <Col></Col>
          <Col className="userInfo">post user details</Col>
        </Row>
      </Container>
    </>
  );
};

export default PostViewComponent;

PostViewComponent.propTypes = {
  id: PropTypes.number,
};
