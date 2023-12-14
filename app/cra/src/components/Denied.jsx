import React from "react";
import { Row, Col } from "react-bootstrap";
import PropTypes from "prop-types";

const Denied = ({ reasonMessage }) => {
  return (
    <>
      <Row>
        <Col></Col>
        <Col>
          <Row>
            <Col>
              <div>{"Your access is denied."}</div>
            </Col>
          </Row>
          <Row>
            <Col>
              <div>{reasonMessage}</div>
            </Col>
          </Row>
        </Col>
        <Col></Col>
      </Row>
    </>
  );
};

export default Denied;

Denied.propTypes = {
  reasonMessage: PropTypes.string,
};
