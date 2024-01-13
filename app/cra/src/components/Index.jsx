import React from "react";
import { Row, Col } from "react-bootstrap";

const Index = () => {
  return (
    <>
      <Row>
        <Col className="col-sm"></Col>
        <Col>{"This is the index page."}</Col>
        <Col className="col-sm"></Col>
      </Row>
    </>
  );
};

export default Index;
