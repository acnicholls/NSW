export const postQueryKeys = {
  post: ["post"],
  getPosts: () => [...postQueryKeys.post, "list"],
  getById: (postId) => [...postQueryKeys.post, postId],
};
