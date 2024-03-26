import { PropTypes } from "prop-types";

const labelTextShape = PropTypes.shape({
  id: PropTypes.string,
  english: PropTypes.string,
  japanese: PropTypes.string,
});

const labelTextViewShape = PropTypes.shape({
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

const postCategoryPillShape = PropTypes.shape({
  id: PropTypes.number,
  title: PropTypes.string,
  description: PropTypes.string,
  countOfPosts: PropTypes.number,
});

const postalCodeShape = PropTypes.shape({
  code: PropTypes.string,
  longitude: PropTypes.number,
  latitude: PropTypes.number,
});

const userShape = PropTypes.shape({
  id: PropTypes.number,
  username: PropTypes.string,
  email: PropTypes.string,
  phone: PropTypes.string,
  role: PropTypes.string,
  postalCode: postalCodeShape,
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
  postCategoryPillShape,
  labelTextShape,
  labelTextViewShape,
};
