import * as yup from "yup";

const labelTextSchema = yup.object().shape({
  id: yup.string(),
  english: yup.string(),
  japanese: yup.string(),
});

const postCategorySchema = yup.object().shape({
  id: yup.number(),
  englishTitle: yup.string(),
  japaneseTitle: yup.string(),
  englishDescription: yup.string(),
  japaneseDescription: yup.string(),
});

const postalCodeSchema = yup.object().shape({
  code: yup.string(),
  longitude: yup.number(),
  latitude: yup.number(),
});

const userSchema = yup.object().shape({
  id: yup.number(),
  name: yup.string(),
  password: yup.string(),
  email: yup.string(),
  status: yup.string(),
  phone: yup.string(),
  postalCode: yup.string(),
  role: yup.string(),
  languagePreference: yup.number(),
});

const postSchema = yup.object().shape({
  id: yup.number(),
  categoryId: yup.number(),
  title: yup.string(),
  description: yup.string(),
  price: yup.number(),
  expiry: yup.date(),
  userId: yup.number(),
  status: yup.string(),
  deleteFlag: yup.bool(),
  postUser: yup.object().shape(userSchema),
});
