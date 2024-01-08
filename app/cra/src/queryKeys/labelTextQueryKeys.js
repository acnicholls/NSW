export const labelTextQueryKeys = {
  labelText: ["label-text"],
  getLabelTexts: () => [...labelTextQueryKeys.labelText],
  getById: (labelTextId) => [...labelTextQueryKeys.labelText, labelTextId],
  getByIdentifier: (labelTextIdentifier) => [
    ...labelTextQueryKeys.labelText,
    labelTextIdentifier,
  ],
  getByGroupIdentifier: (groupIdentifier) => [
    ...labelTextQueryKeys.labelText,
    "group",
    groupIdentifier,
  ],
};
