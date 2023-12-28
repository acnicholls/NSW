import React, { useState } from "react";
import { Row, Col } from "react-bootstrap";
import PropTypes from "prop-types";
//import { useUserContext } from "../../contexts/UserContext";
import { PostPageVariantEnum } from "../../constants/PostPageVariantEnum";
import { postShape } from "../../shapes/shapes";

const PostListPostComponent = ({ variant, currentPost }) => {
  const postbuttons =
    variant === PostPageVariantEnum.Main ? (
      <></>
    ) : (
      <>
        <button></button>
      </>
    );
  return (
    <>
      <Row>
        <Col>post image</Col>
        <Col>{currentPost.title}</Col>
        <Col>{postbuttons}</Col>
      </Row>
    </>
  );
};

export default PostListPostComponent;

PostListPostComponent.propTypes = {
  variant: PropTypes.oneOf(Object.keys(PostPageVariantEnum)),
  currentPost: postShape,
};
