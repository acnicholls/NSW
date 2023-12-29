import React from "react";
import PropTypes from "prop-types";
import { PostPageVariantEnum } from "../../constants/PostPageVariantEnum";
import PostListComponentMainVariant from "./PostListComponentMainVariant";
import PostListComponentMyVariant from "./PostListComponentMyVariant";

const PostListComponent = ({ variant }) => {
  const returnValue =
    variant === PostPageVariantEnum.Main ? (
      <PostListComponentMainVariant />
    ) : (
      <PostListComponentMyVariant />
    );
  return returnValue;
};

export default PostListComponent;

PostListComponent.propTypes = {
  variant: PropTypes.oneOf(Object.keys(PostPageVariantEnum)),
};
