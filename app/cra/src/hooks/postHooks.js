import { useQuery } from "react-query";
import { postQueryKeys } from "../queryKeys/postQueryKeys";

import { getPosts, getPostById } from "../services/PostService";

const usePostList = (isDisabled, onSuccess, onError) => {
  return useQuery(postQueryKeys.getPosts, async () => await getPosts(), {
    staleTime: Infinity,
    isDisabled: isDisabled,
    onSuccess: (data) => onSuccess && onSuccess(data),
    onError: (error) => onError && onError(error),
  });
};

const usePostInfo = (postId, isDisabled, onSuccess, onError) => {
  return useQuery(
    postQueryKeys.getById(postId),
    async () => await getPostById(postId),
    {
      staleTime: Infinity,
      enabled: postId !== 0,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};

export { usePostInfo, usePostList };
