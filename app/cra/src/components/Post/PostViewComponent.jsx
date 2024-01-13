import "../../App.css";

import React, { useState } from "react";
import { PropTypes } from "prop-types";
/* 
  Layout
*/
import {
  Container,
  Row,
  Col,
  FormCheck,
  Button,
  Card,
  CardBody,
  CardFooter,
  CardHeader,
  Carousel,
  CarouselItem,
} from "react-bootstrap";
import FormCheckInput from "react-bootstrap/esm/FormCheckInput";
import FormCheckLabel from "react-bootstrap/esm/FormCheckLabel";
/*
Contexts
*/
import { useUserContext } from "../../contexts/UserContext";
/* 
  Components
*/
/*
  Hooks
*/
import { usePostInfo } from "../../hooks/postHooks";
import { useParams } from "react-router";
import SpacerRow from "../SpacerRow";
/* */
/* */

/*
 - the post view component
 - allows the user to view a post
*/
const PostViewComponent = () => {
  let params = useParams();
  console.log("inside Post View");
  console.log("params: ", JSON.stringify(params));
  const [post, setPost] = useState(null);

  // define success/error of query
  var queryIsDisabled = false;
  const onSuccess = (data) => {
    console.log("PostViewComponent.OnSuccess", data);
    setPost(data.data);
    queryIsDisabled = true;
  };

  const onError = (error) => {
    console.log("PostViewComponent.onError", error);
  };

  // get post details from react-query
  console.log("querying for postInfo: ", params?.id);
  const { error, isLoading, isError } = usePostInfo(
    params?.id,
    queryIsDisabled,
    onSuccess,
    onError
  );

  // handle loading/errors of query
  if (isLoading) {
    return <>Loading...</>;
  }

  if (isError) {
    return (
      <>
        <Container>
          <Row>
            <Col>Error: {error}</Col>
          </Row>
        </Container>
      </>
    );
  }

  // if there's no images, create a default one
  const defaultNoImageCarouselItem = `${process.env.PUBLIC_URL}/images/pollyStretch.jpg`;
  const showDefaultItem = true;

  // need a hook to get the posts images

  var images = [
    `${process.env.PUBLIC_URL}/images/pollyStretch.jpg`,
    `${process.env.PUBLIC_URL}/images/scrollReddit.jpg`,
  ];
  var carouselItems =
    images && images.length > 0 ? images : [defaultNoImageCarouselItem];
  var carouselInterval = carouselItems.length === 1 ? null : 4200;
  // need a loop here to create carousel items from the post's images
  const carouselObject = (
    <>
      <Carousel
        className="h-100"
        interval={carouselInterval}
        pause="hover"
        wrap
        touch
        keyboard
        controls
        indicators
        slide
        defaultActiveIndex={0}
      >
        {carouselItems &&
          carouselItems.length > 0 &&
          carouselItems.map((x) => {
            return (
              <CarouselItem>
                <img className="splashImage" src={x} alt={"postImage"} />
              </CarouselItem>
            );
          })}
      </Carousel>
    </>
  );

  return (
    <>
      <Card>
        <CardHeader>
          <SpacerRow />
          <Row>
            <Col className="itemTitle">{post?.title}</Col>
            <Col className="text-right">{`¥${post?.price}`}</Col>
          </Row>
          <SpacerRow />
        </CardHeader>
        <CardBody>
          <Container fluid className="h-75 text-center">
            <SpacerRow />
            <Row>
              <Col className="h-50">{carouselObject}</Col>
            </Row>
            <SpacerRow />
            <Row>
              <Col className="itemInfo h-25">{post?.description}</Col>
            </Row>
            <SpacerRow />
          </Container>
        </CardBody>
        <CardFooter>
          <SpacerRow />
          <Row>
            <Col>&nbsp;</Col>
            <Col className="userInfo">post user details</Col>
          </Row>
          <SpacerRow />
        </CardFooter>
      </Card>
    </>
  );
};

export default PostViewComponent;

PostViewComponent.propTypes = {
  id: PropTypes.number,
};
