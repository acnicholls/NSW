import React, { useState } from "react";
import { useUserPostList } from "../../hooks/postHooks";
import { useUserContext } from "../../contexts/UserContext";
import PostListPostComponent from "./PostListPostComponent";
import { Row, Col } from "react-bootstrap";

const PostListComponentMyVariant = ({}) => {
  const [isQueryDisabled, setIsQueryDisabled] = useState(false);
  const { user } = useUserContext();
  console.log("user in PostList MyVariant", user);

  const noDataFound = (
    <>
      <Row>
        <Col>No data found.</Col>
      </Row>
    </>
  );

  function onSuccess(data) {
    console.log(data);
    if (data.status === 200) {
      console.log(
        `success retrieving post list for 'my' variant of PostListComponent`
      );
      setIsQueryDisabled(true);
    }
  }
  function onError(error) {
    console.log(error);
  }
  const { data, error, isLoading, isError } = useUserPostList(
    user.id,
    isQueryDisabled,
    onSuccess,
    onError
  );

  if (isLoading) {
    return <>Loading...</>;
  }

  if (isError || data.status !== 200) {
    return <>{error.message}</>;
  }
  console.log("user posts List", data);
  const returnValue =
    data.data && data.data.length > 0
      ? data.data.map((x) => (
          <PostListPostComponent key={x.id} currentPost={x} />
        ))
      : noDataFound;

  return returnValue;
};

export default PostListComponentMyVariant;

PostListComponentMyVariant.propTypes = {};
