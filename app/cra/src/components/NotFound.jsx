import React from "react";
import { Row, Col } from "react-bootstrap";

const NotFound = () => {
  return (
    <>
      <Col sm={12} md={12} lg={12}>
        <Row>
          <span>Your request is invalid</span>
        </Row>
      </Col>
    </>
  );
};

export default NotFound;
