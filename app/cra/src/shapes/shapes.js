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
  cookiename: PropTypes.string,
  id: PropTypes.number,
  name: PropTypes.string,
  firstName: PropTypes.string,
  lastName: PropTypes.string,
  username: PropTypes.string,
  role: PropTypes.string,
  languagePreference: PropTypes.number,
  isAuthenticated: PropTypes.bool,
});

const postShape = PropTypes.shape({
  id: PropTypes.number,
  categoryId: PropTypes.number,
  title: PropTypes.string,
  description: PropTypes.string,
  price: PropTypes.number,
  expiry: PropTypes.string,
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
