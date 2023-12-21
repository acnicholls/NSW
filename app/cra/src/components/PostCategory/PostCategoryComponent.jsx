import React from "react";
import { PropTypes } from "prop-types";
import { useUserContext } from "../../contexts/UserContext";
import { useParams } from "react-router";
import { Row, Col, FormCheck, Button } from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";
import { ViewModes } from "../../constants/ViewModes";
import { postCategoryShape } from "../../shapes/shapes";

const PostCategoryComponent = ({
  id,
  currentPostCategory,
  viewMode,
  onCancel,
  onSave,
}) => {
  const { user } = useUserContext();
  const { categoryId } = useParams();

  const listMode = viewMode === ViewModes.view && id !== null;
  // if list mode, get the list

  const viewModeListReturnValue = <></>;
  const viewModeSingleReturnValue = <></>;

  const viewModeReturnValue = listMode
    ? viewModeListReturnValue
    : viewModeSingleReturnValue;
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
  viewMode: PropTypes.string,
  onSave: PropTypes.func,
  onCancel: PropTypes.func,
};
