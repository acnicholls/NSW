import { PropTypes } from "prop-types";

const labelTextShape = PropTypes.shape({
  id: PropTypes.string,
  english: PropTypes.string,
  japanese: PropTypes.string,
});

const postCategoryShape = PropTypes.shape({
  id: PropTypes.number,
  englishTitle: PropTypes.string,
  japaneseTitle: PropTypes.string,
  englishDescription: PropTypes.string,
  japaneseDescription: PropTypes.string,
});

const postalCodeShape = PropTypes.shape({
  code: PropTypes.string,
  longitude: PropTypes.number,
  latitude: PropTypes.number,
});

const userShape = PropTypes.shape({
  id: PropTypes.number,
  name: PropTypes.string,
  password: PropTypes.string,
  email: PropTypes.string,
  status: PropTypes.string,
  phone: PropTypes.string,
  postalCode: PropTypes.string,
  role: PropTypes.string,
  languagePreference: PropTypes.number,
});

const postShape = PropTypes.shape({
  id: PropTypes.number,
  categoryId: PropTypes.number,
  title: PropTypes.string,
  description: PropTypes.string,
  price: PropTypes.number,
  expiry: PropTypes.date,
  userId: PropTypes.number,
  status: PropTypes.string,
  deleteFlag: PropTypes.bool,
  postUser: PropTypes.instanceOf(userShape),
});

export {
  userShape,
  postShape,
  postalCodeShape,
  postCategoryShape,
  labelTextShape,
};
