import React, { useState } from "react";
import { Row, Col } from "react-bootstrap";
import PropTypes from "prop-types";
//import { useUserContext } from "../../contexts/UserContext";
import { PostPageVariantEnum } from "../../constants/PostPageVariantEnum";
import { usePostList } from "../../hooks/postHooks";
import PostListPostComponent from "./PostListPostComponent";

const PostListComponent = ({ variant }) => {
  const [isQueryDisabled, setIsQueryDisabled] = useState(false);
  const [mainPostPageListValue, setMainPostPageListValue] = useState(null);
  const message =
    variant === PostPageVariantEnum.My
      ? "this is the MY posts page"
      : "this is the posts page";

  function onSuccess(data) {
    console.log("success retrieving post list");
    console.log(data);
    setMainPostPageListValue(data.data);
    setIsQueryDisabled(true);
  }
  function onError(error) {
    console.log(error);
  }
  const { data, error, isLoading, isError } = usePostList(
    isQueryDisabled,
    onSuccess,
    onError
  );

  if (isLoading) {
    return <>Loading...</>;
  }

  if (isError) {
    return <>{error.message}</>;
  }
  console.log("posts List", data);
  // var { user } = useUserContext();
  const mainPostsPageReturnValue = data.data.map((x) => (
    <PostListPostComponent key={x.id} currentPost={x} />
  ));

  const myPostsPageReturnValue = (
    <>
      <Row>
        <Col></Col>
        <Col>{message}</Col>
        <Col></Col>
      </Row>
    </>
  );

  const defaultReturnValue = (
    <>
      <Row>
        <Col></Col>
        <Col>{message}</Col>
        <Col></Col>
      </Row>
    </>
  );

  const variantReturnValue =
    variant === PostPageVariantEnum.Main
      ? mainPostsPageReturnValue
      : myPostsPageReturnValue;
  console.log("variant value:", variant);
  const returnValue = variantReturnValue ?? defaultReturnValue;
  console.log("returning: ", returnValue);
  return returnValue;
};

export default PostListComponent;

PostListComponent.propTypes = {
  variant: PropTypes.oneOf(Object.keys(PostPageVariantEnum)),
};
