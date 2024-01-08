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
    labelTextQueryKeys.getLabelTextById(labelTextId),
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

const useLabelTextByIdentifier = (
  labelTextIdentifier,
  isDisabled,
  onSuccess,
  onError
) => {
  return useQuery(
    labelTextQueryKeys.getByIdentifier(labelTextIdentifier),
    async () => await service.getLabelTextByIdentifier(labelTextIdentifier),
    {
      staleTime: Infinity,
      enabled: labelTextIdentifier !== "",
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
    labelTextQueryKeys.getByGroupIdentifier(groupIdentifier),
    async () => await service.getAllLabelTextByPageIdentifier(groupIdentifier),
    {
      staleTime: Infinity,
      enabled: groupIdentifier !== "",
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};
export {
  useLabelTextById,
  useLabelTextByIdentifier,
  useLabelTextList,
  useLabelTextByGroupIdentifier,
};
