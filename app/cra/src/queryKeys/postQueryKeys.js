export const postQueryKeys = {
  post: ["post"],
  getPosts: () => [...postQueryKeys.post, "list"],
  getById: (postId) => [...postQueryKeys.post, postId],
  getUserPosts: (userId) => [...postQueryKeys.post, "user", userId],
  getCategoryPosts: (categoryId) => [
    ...postQueryKeys.post,
    "category",
    categoryId,
  ],
};
