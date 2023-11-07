export const labelTextQueryKeys = {
  labelText: ["label-text"],
  getLabelTexts: () => [...labelTextQueryKeys.labelText],
  getById: (labelTextId) => [...labelTextQueryKeys.labelText, labelTextId],
};
