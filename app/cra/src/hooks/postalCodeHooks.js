import { useQuery } from "react-query";
import { postalCodeQueryKeys } from "../queryKeys/postalCodeQueryKeys";

import {
  getPostalCodes,
  getPostalCodeById,
} from "../services/PostalCodeService";

const usePostalCodeList = (isDisabled, onSuccess, onError) => {
  return useQuery(
    postalCodeQueryKeys.getPostalCodes,
    async () => await getPostalCodes(),
    {
      staleTime: Infinity,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};

const usePostalCodeById = (postalCodeId, isDisabled, onSuccess, onError) => {
  return useQuery(
    postalCodeQueryKeys.getPostalCodeById(postalCodeId),
    async (postalCodeId) => await getPostalCodeById(postalCodeId),
    {
      staleTime: Infinity,
      enabled: postalCodeId > 0,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};
export { usePostalCodeById, usePostalCodeList };
