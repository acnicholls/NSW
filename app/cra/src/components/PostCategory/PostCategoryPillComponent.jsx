import React from "react";
import { Col, Row, Button } from "react-bootstrap";
import PropTypes from "prop-types";
import { postCategoryShape } from "../../shapes/shapes";
import { useUserContext } from "../../contexts/UserContext";
import { useNavigate } from "react-router";
import routes from "../../constants/RouteConstants";
import { LanguagePreference } from "../../constants/LanguagePreference";

/**
 * this component will display basic info about a post category, used for the category list page
 * @param {categoryInfo} categoryInfo informionat about the displayed category
 * @returns PostCategoryPillComponent
 */
const PostCategoryPillComponent = ({ categoryInfo }) => {
  const navigate = useNavigate();

  const onCategoryButtonClick = () => {
    navigate(`${routes.frontend.posts}/category/${categoryInfo.id}`);
  };
  return (
    <>
      <Button onClick={onCategoryButtonClick}>
        <Row>
          <Col>{categoryInfo.name}</Col>
          <Col>{categoryInfo.countOfPosts}</Col>
        </Row>
        <Row>
          <Col colspan={2}>{categoryInfo.description}</Col>
        </Row>
      </Button>
    </>
  );
};

export default PostCategoryPillComponent;

PostCategoryPillComponent.propTypes = {
  categoryInfo: postCategoryShape,
};
