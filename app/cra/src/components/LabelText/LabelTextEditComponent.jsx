import React from "react";
import { PropTypes } from "prop-types";
import { useUserContext } from "../../contexts/UserContext";
//import { useLabelTextContext } from "../contexts/LabelTextContext";
import {
  Row,
  Col,
  FormCheck,
  Button,
  Input,
  FormGroup,
  FormLabel,
  FormControl,
  FormText,
} from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";
import { labelTextShape } from "../../shapes/shapes";
import { RoleEnum } from "../../constants/RoleEnum";
import { ViewModes } from "../../constants/ViewModes";
import { useForm } from "react-hook-form";
// import { yupResolver } from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { labelTextSchema } from "../../schemas/schemas";

const LabelTextComponent = ({
  currentLabelText,
  viewMode,
  onCancel,
  onSave,
}) => {
  // const {
  //   labelText,
  //   setLabelText,
  //   getLabelText,
  //   getLabelTextById,
  //   labelTextList,
  //   setLabelTextList,
  // } = useLabelTextContext();
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

  const identifierControl = (
    <>
      <FormGroup controlId="identifier">
        <FormLabel>Identifier: </FormLabel>
        <FormControl
          as="input"
          type="text"
          control={control}
          placeholder="An identifier is required."
          {...register("identifier", { required: true, maxLength: 50 })}
        />
      </FormGroup>
    </>
  );
  const englishControl = (
    <>
      <FormGroup controlId="english">
        <FormLabel>English: </FormLabel>
        <FormControl
          as="input"
          type="text"
          control={control}
          placeholder="English label text is required."
          {...register("english", { required: true, maxLength: 4000 })}
        />
      </FormGroup>
    </>
  );
  const japaneseControl = (
    <>
      <FormGroup controlId="japanese">
        <FormLabel>Japanese: </FormLabel>
        <FormControl
          as="input"
          type="text"
          control={control}
          placeholder="Japanese label text is required."
          {...register("japanese", { required: true, maxLength: 4000 })}
        />
      </FormGroup>
    </>
  );

  const viewModeReturnValue = (
    <>
      <Row>
        <Col> </Col>
        <Col>{currentLabelText.identifier}</Col>
      </Row>
      <Row>
        <Col>{currentLabelText.english}</Col>
        <Col>{currentLabelText.japanese}</Col>
      </Row>
    </>
  );

  var editModeReturnValue = (
    <>
      <form
        onSubmit={handleSubmit((formData, event) =>
          customSubmit(formData, event)
        )}
      >
        <Row>
          <Col> </Col>
          <Col>{identifierControl}</Col>
        </Row>
        <Row>
          <Col>{englishControl}</Col>
          <Col>{japaneseControl}</Col>
        </Row>
      </form>
    </>
  );

  /*
    decide what to return to the user.
  */
  if (user.role !== RoleEnum.Admin) {
    return viewModeReturnValue;
  }

  switch (viewMode) {
    case ViewModes.edit: {
      return editModeReturnValue;
    }
    default: {
      return viewModeReturnValue;
    }
  }
};

export default LabelTextComponent;

LabelTextComponent.propTypes = {
  currentLabelText: labelTextShape,
  viewMode: PropTypes.string,
  onSave: PropTypes.func,
  onCancel: PropTypes.func,
};
