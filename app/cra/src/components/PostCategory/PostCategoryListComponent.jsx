import React, { useState } from "react";
import { Col, Row, Button, Container } from "react-bootstrap";
import PropTypes from "prop-types";
import { postCategoryShape } from "../../shapes/shapes";
import { useUserContext } from "../../contexts/UserContext";
import { useParams } from "react-router";
import { useNavigate } from "react-router";
import routes from "../../constants/RouteConstants";
import { LanguagePreference } from "../../constants/LanguagePreference";
import { useLabelTextByGroupIdentifier } from "../../hooks/labelTextHooks";
import { usePostCategoryList } from "../../hooks/postCategoryHooks";
import PostCategoryComponent from "./PostCategoryComponent";

/**
 * this component will display the list of post categories
 * @param {categoryInfo} categoryInfo informionat about the displayed category
 * @returns PostCategoryListComponent
 */
const PostCategoryListComponent = () => {
  const { categoryId } = useParams();
  const navigate = useNavigate();
  const [postList, setPostList] = useState([]);

  const onCategoryButtonClick = () => {};
  // get list from API

  const { isLoading, isError, data, error } = usePostCategoryList();

  if (isLoading) {
    return <>Loading...</>;
  }

  if (isError && data.status !== 200) {
    console.log(error);
    return <>{error.message}</>;
  }

  if (data.status === 200) {
    setPostList(data.data);
  }

  /**
   * the return
   */
  return (
    <>
      <Container>
        <Row>
          <Col>
            <PostCategoryComponent />
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default PostCategoryListComponent;

PostCategoryListComponent.propTypes = {};
