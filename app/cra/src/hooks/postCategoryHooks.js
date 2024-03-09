import { useQuery } from "react-query";
import { postCategoryQueryKeys } from "../queryKeys/postCategoryQueryKeys";

import service from "../services/PostCategoryService";

const usePostCategoryList = (isDisabled, onSuccess, onError) => {
  return useQuery(
    postCategoryQueryKeys.getPostCategories(),
    async () => await service.getPostCategories(),
    {
      staleTime: Infinity,
      enabled: !isDisabled,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};
export { usePostCategoryList };
