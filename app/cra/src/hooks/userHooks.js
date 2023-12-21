import { useQuery } from "react-query";
import { userQueryKeys } from "../queryKeys/userQueryKeys";

import service from "../services/UserService";

const useUserInfo = (isDisabled, onSuccess, onError) => {
  return useQuery(
    userQueryKeys.getUserInfo(),
    async () => await service.getUserInfo(),
    {
      staleTime: Infinity,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};

export { useUserInfo };
