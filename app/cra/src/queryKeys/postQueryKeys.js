export const postQueryKeys = {
  post: ["post"],
  getPosts: () => [...postQueryKeys.post],
  getById: (postId) => [...postQueryKeys.post, postId],
};
