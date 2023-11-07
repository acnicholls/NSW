import { useQuery } from "react-query";
import { labelTextQueryKeys } from "../queryKeys/labelTextQueryKeys";

import { getLabelText, getLabelTextById } from "../services/LabelTextService";

const useLabelTextList = (isDisabled, onSuccess, onError) => {
  return useQuery(
    labelTextQueryKeys.getLabelTexts,
    async () => await getLabelText(),
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
    async () => await getLabelTextById(labelTextId),
    {
      staleTime: Infinity,
      enabled: labelTextId !== "" || labelTextId !== 0,
      isDisabled: isDisabled,
      onSuccess: (data) => onSuccess && onSuccess(data),
      onError: (error) => onError && onError(error),
    }
  );
};

export { useLabelTextById, useLabelTextList };
