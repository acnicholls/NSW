import React from "react";
import { Container, Row, Col } from "react-bootstrap";

const TitleBarComponent = () => {
  return (
    <>
      <Container>
        <Row>
          <Col></Col>
          <Col>
            <h1>Natsuko's Sharing Website</h1>
          </Col>
          <Col></Col>
        </Row>
      </Container>
    </>
  );
};

export default TitleBarComponent;
