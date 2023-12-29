import React, { useState } from "react";
import { usePostList } from "../../hooks/postHooks";
import PostListPostComponent from "./PostListPostComponent";
import { PostPageVariantEnum } from "../../constants/PostPageVariantEnum";

const PostListComponentMainVariant = ({}) => {
  const [isQueryDisabled, setIsQueryDisabled] = useState(false);

  function onSuccess(data) {
    console.log(
      `success retrieving post list for 'main' variant of PostListComponent`
    );
    console.log(data);
    setIsQueryDisabled(true);
  }
  function onError(error) {
    console.log(error);
  }
  const { data, error, isLoading, isError } = usePostList(
    isQueryDisabled,
    onSuccess,
    onError
  );

  if (isLoading) {
    return <>Loading...</>;
  }

  if (isError || data.status !== 200) {
    return <>{error.message}</>;
  }
  console.log("posts List", data);
  const returnValue = data.data.map((x) => (
    <PostListPostComponent
      key={x.id}
      currentPost={x}
      variant={PostPageVariantEnum.Main}
    />
  ));

  return returnValue;
};

export default PostListComponentMainVariant;

PostListComponentMainVariant.propTypes = {};
