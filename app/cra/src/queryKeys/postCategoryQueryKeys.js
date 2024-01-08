export const postCategoryQueryKeys = {
  postCategory: ["post-category"],
  getPostCategories: () => [...postCategoryQueryKeys.postCategory],
  getById: (postCategoryId) => [
    ...postCategoryQueryKeys.postCategory,
    postCategoryId,
  ],
};
