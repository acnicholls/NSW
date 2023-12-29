import { useQuery } from "react-query";
import { labelTextQueryKeys } from "../queryKeys/labelTextQueryKeys";

import service from "../services/LabelTextService";

const useLabelTextList = (isDisabled, onSuccess, onError) => {
  return useQuery(
    labelTextQueryKeys.getLabelTexts,
    async () => await service.getLabelTexts(),
    {
      staleTime: Infinity,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};

const useLabelTextById = (labelTextId, isDisabled, onSuccess, onError) => {
  return useQuery(
    labelTextQueryKeys.getLabelTextById,
    async () => await service.getLabelTextById(labelTextId),
    {
      staleTime: Infinity,
      enabled: labelTextId !== "" || labelTextId !== 0,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};

const useLabelTextByGroupIdentifier = (
  groupIdentifier,
  isDisabled,
  onSuccess,
  onError
) => {
  return useQuery(
    labelTextQueryKeys.getByGroupIdentifier,
    async (groupIdentifier) =>
      await service.getLabelTextByPageIdentifier(groupIdentifier),
    {
      staleTime: Infinity,
      enabled: groupIdentifier !== "",
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};
export { useLabelTextById, useLabelTextList, useLabelTextByGroupIdentifier };
