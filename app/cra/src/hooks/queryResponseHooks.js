import { Row, Col } from "react-bootstrap";

const useIsLoading = (isLoading) => {
  if (isLoading) {
    return (
      <>
        <Row>
          <Col>Loading...</Col>
        </Row>
      </>
    );
  }
};

const useIsError = (isError, error) => {
  if (isError) {
    console.log(error);
    return (
      <>
        <Row>
          <Col>error.message</Col>
        </Row>
      </>
    );
  }
};

const useNoDataFound = () => {
  return (
    <>
      <Row>
        <Col>No data found.</Col>
      </Row>
    </>
  );
};

export default { useIsLoading, useIsError, useNoDataFound };
