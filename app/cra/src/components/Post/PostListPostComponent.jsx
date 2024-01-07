import React, { useState } from "react";
import {
  Container,
  Row,
  Col,
  Button,
  Card,
  CardBody,
  CardTitle,
  CardText,
  CardImg,
  CardFooter,
} from "react-bootstrap";
import PropTypes from "prop-types";
import { useUserContext } from "../../contexts/UserContext";
import { PostPageVariantEnum } from "../../constants/PostPageVariantEnum";
import { postShape } from "../../shapes/shapes";
import { useNavigate } from "react-router";
import routes from "../../constants/RouteConstants";

const PostListPostComponent = ({ variant, currentPost }) => {
  const { user } = useUserContext();
  const [displayPost, setDisplayPost] = useState(currentPost);
  const navigate = useNavigate();

  // set up the delete mutation

  // edit post click => navigate to post edit URL
  const onEditButtonClick = () => {
    navigate(`${routes.frontend.posts}/edit/${currentPost.id}`);
  };

  // delete post click => send API call
  const onDeleteButtonClick = () => {};

  // view "button" click => send to post view URL
  const onViewButtonClick = () => {
    navigate(`${routes.frontend.posts}/${currentPost.id}`);
  };

  const postUserButtons = (
    <>
      <Button size="sm" variant="info" onClick={onEditButtonClick}>
        Edit
      </Button>
      <Button size="sm" variant="danger" onClick={onDeleteButtonClick}>
        Delete
      </Button>
    </>
  );

  const postViewButton = (
    <>
      <Button size="sm" variant="primary" onClick={onViewButtonClick}>
        View
      </Button>
    </>
  );

  const postbuttons =
    variant === PostPageVariantEnum.Main ? (
      <>{postViewButton}</>
    ) : (
      <>
        {postViewButton}
        {postUserButtons}
      </>
    );
  return (
    <>
      <Row>
        <Col>
          <Card>
            <CardBody>
              <Container fluid={true}>
                <Row>
                  <Col>
                    <CardTitle>{displayPost.title}</CardTitle>
                  </Col>
                  <Col>post image here</Col>
                </Row>
                <Row>
                  <Col>
                    <CardText>{displayPost.description}</CardText>
                  </Col>
                </Row>
              </Container>
            </CardBody>
            <CardFooter>
              <Container fluid="true">
                <Row>
                  <Col sm={6} md={4} lg={2}>
                    {displayPost.price}
                  </Col>
                  <Col sm={6} md={8} lg={10} className="text-end">
                    {postbuttons}
                  </Col>
                </Row>
              </Container>
            </CardFooter>
          </Card>
        </Col>
      </Row>
    </>
  );
};

export default PostListPostComponent;

PostListPostComponent.propTypes = {
  variant: PropTypes.oneOf(Object.keys(PostPageVariantEnum)),
  currentPost: postShape,
};
