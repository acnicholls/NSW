import React, { useState, useEffect } from "react";
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
import PostCategoryPillComponent from "./PostCategoryPillComponent";
import { useIsLoading, useIsError } from "../../hooks/queryResponseHooks";

/**
 * this component will display the list of post categories
 * @param {categoryInfo} categoryInfo informionat about the displayed category
 * @returns PostCategoryListComponent
 */
const PostCategoryListComponent = () => {
  console.log("starting post category list component");
  const { categoryId } = useParams();
  const navigate = useNavigate();
  const [postList, setPostList] = useState([]);

  const onCategoryButtonClick = () => {};

  const onSuccess = (data) => {
    console.log("returned data:", data);
    //if (data.status === 200) {
    setPostList(data.data);
    //}
  };

  const onError = (error) => {
    console.log(error);
  };

  // get list from API
  const { isLoading, isError, data, error } = usePostCategoryList(
    postList.length > 0,
    onSuccess,
    onError
  );

  console.log("isLoading:", isLoading);
  console.log("isError:", isError);

  // handle query response
  useIsLoading(isLoading);
  useIsError(isError, error);

  useEffect(() => {
    if (!isLoading) {
      setPostList(data.data);
    }
  }, [data, isLoading]);

  console.log("completing post category list component", postList);
  /**
   * the return
   */
  return (
    <>
      <Container>
        <Row>
          <Col>
            {postList.map((postCategory) => (
              <PostCategoryPillComponent
                key={postCategory.id}
                categoryInfo={postCategory}
              />
            ))}
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default PostCategoryListComponent;

PostCategoryListComponent.propTypes = {};
