import React from "react";
import { PropTypes } from "prop-types";
import { useUserContext } from "../contexts/UserContext";
import { useLabelTextContext } from "../../contexts/LabelTextContext";
import { Row, Col, FormCheck, Button } from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";

const ViewLabelText = ({ id }) => {
  const {
    labelText,
    setLabelText,
    getLabelText,
    getLabelTextById,
    labelTextList,
    setLabelTextList,
  } = useLabelTextContext();
  const { user } = useUserContext();

  return <>the labelText or labelText list goes here.</>;
};

export default ViewLabelText;

ViewLabelText.propTypes = {
  id: PropTypes.number,
};
