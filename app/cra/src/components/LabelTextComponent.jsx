import React from "react";
import { PropTypes } from "prop-types";
import { useUserContext } from "../contexts/UserContext";
import { useLabelTextContext } from "../contexts/LabelTextContext";
import { Row, Col, FormCheck, Button } from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";
import { labelTextShape } from "../shapes/shapes";
import { RoleEnum } from "../constants/RoleEnum";
import { ViewModes } from "../constants/ViewModes";
import { useForm } from "react-hook-form";
import { yupResolver } from "yup";
import { labelTextSchema } from "../schemas/schemas";

const LabelTextComponent = ({
  currentLabelText,
  viewMode,
  onCancel,
  onSave,
}) => {
  const {
    labelText,
    setLabelText,
    getLabelText,
    getLabelTextById,
    labelTextList,
    setLabelTextList,
  } = useLabelTextContext();
  const { user } = useUserContext();

  /*
    Declare the form
  */
  const {
    control,
    register,
    reset,
    handleSubmit,
    formState: { isValid },
    trigger,
  } = useForm({
    mode: "onChange",
    defaultValues: {
      id: currentLabelText.id,
      english: currentLabelText.english,
      japanese: currentLabelText.japanese,
    },
    resolver: yupResolver(labelTextSchema),
    criteriaMode: "all",
  });

  /*
    craft the form submit
  */
  const customSubmit = async (formData, event) => {};

  const viewModeReturnValue = (
    <>
      <Row>
        <Col>Identifier:</Col>
        <Col>{labelText.id}</Col>
      </Row>
      <Row>
        <Col>English: {labelText.english}</Col>
        <Col>Japanese: {labelText.japanese}</Col>
      </Row>
    </>
  );

  var editModeReturnValue = (
    <>
      <form
        onSubmit={handleSubmit((formData, event) =>
          customSubmit(formData, event)
        )}
      ></form>
    </>
  );

  /*
    decide what to return to the user.
  */
  if (user.role !== RoleEnum.Admin) {
    return viewModeReturnValue;
  }

  if (viewMode === ViewModes.edit) {
    return editModeReturnValue;
  } else {
    return viewModeReturnValue;
  }

  return <>the labelText or labelText list goes here.</>;
};

export default LabelTextComponent;

LabelTextComponent.propTypes = {
  currentLabelText: labelTextShape,
  viewMode: PropTypes.string,
  onSave: PropTypes.func,
  onCancel: PropTypes.func,
};
