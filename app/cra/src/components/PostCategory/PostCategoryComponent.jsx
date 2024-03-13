import React from "react";
import { PropTypes } from "prop-types";
import { useUserContext } from "../../contexts/UserContext";

import { Row, Col, FormCheck, Button, Container } from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";
import { ViewModes } from "../../constants/ViewModes";
import { postCategoryShape } from "../../shapes/shapes";
import { usePostCategoryList } from "../../hooks/postCategoryHooks";
import PostCategoryPillComponent from "./PostCategoryPillComponent";
/**
 *
 * @param {number} id - the id of the category
 * @param {[postCategoryShape]} postList - a category object
 * @param {string} viewMode - which mode to view, list or edit
 * @param {function} onSave - callback for save
 * @param {function} onCancel - callback for cancel
 * @returns
 */
const PostCategoryComponent = ({
  id,
  currentPostCategory,
  viewMode,
  onCancel,
  onSave,
}) => {
  const { user } = useUserContext();

  const viewModeReturnValue = (
    <>
      <Row>
        <Col>{currentPostCategory.englishTitle}</Col>
        <Col>{currentPostCategory.japaneseTitle}</Col>
      </Row>
      <Row>
        <Col>{currentPostCategory.englishDescription}</Col>
        <Col>{currentPostCategory.japaneseDescription}</Col>
      </Row>
    </>
  );

  const editModeReturnValue = <></>;

  switch (viewMode) {
    case ViewModes.edit: {
      return editModeReturnValue;
    }
    case ViewModes.view:
    default: {
      return viewModeReturnValue;
    }
  }
};

export default PostCategoryComponent;

PostCategoryComponent.propTypes = {
  id: PropTypes.number,
  currentPostCategory: postCategoryShape,
  viewMode: PropTypes.oneOf(ViewModes),
  onSave: PropTypes.func,
  onCancel: PropTypes.func,
};
