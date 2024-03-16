import { useQuery } from "react-query";
import { postQueryKeys } from "../queryKeys/postQueryKeys";

import service from "../services/PostService";

const usePostList = (isDisabled, onSuccess, onError) => {
  return useQuery(
    postQueryKeys.getPosts(),
    async () => await service.getPosts(),
    {
      staleTime: Infinity,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};

const useUserPostList = (userId, isDisabled, onSuccess, onError) => {
  return useQuery(
    postQueryKeys.getUserPosts(userId),
    async () => await service.getPostsByUserId(userId),
    {
      staleTime: Infinity,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};

const usePostInfo = (postId, isDisabled, onSuccess, onError) => {
  return useQuery(
    postQueryKeys.getById(postId),
    async () => await service.getPostById(postId),
    {
      staleTime: Infinity,
      enabled: postId !== "" || postId !== 0,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};

export { usePostInfo, usePostList, useUserPostList };
