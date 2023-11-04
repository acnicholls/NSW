import React from "react";
import { PropTypes } from "prop-types";
import { useUserContext } from "../contexts/UserContext";
import { usePostalCodeContext } from "../../contexts/PostalCodeContext";
import { Row, Col, FormCheck, Button } from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";

const ViewPostalCode = ({ id }) => {
  const {
    postalCode,
    setPostalCode,
    getPostalCode,
    getPostalCodeById,
    postalCodeList,
    setPostalCodeList,
  } = usePostalCodeContext();
  const { user } = useUserContext();

  return <>the postalCode or postalCode list goes here.</>;
};

export default ViewPostalCode;

ViewPostalCode.propTypes = {
  id: PropTypes.number,
};
