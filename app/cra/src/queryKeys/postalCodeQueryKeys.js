export const postalCodeQueryKeys = {
  postalCode: ["postal-code"],
  getPostalCodes: () => [...postalCodeQueryKeys.postalCode],
  getById: (postalCodeId) => [...postalCodeQueryKeys.postalCode, postalCodeId],
};
