import PropTypes from "prop-types";
import { useUserContext } from "../../contexts/UserContext";
import routes from "../../constants/RouteConstants";

/**
 * When the router navigates to the internal URL, this component instead redirects the browser to an external URL.
 * @param {boolean} isPrivate - used to handle authentication for external links, defaults to false.
 * @param {string} link - the external link to send the user to when the internal route is navigated to.
 * @returns browser redirect
 */
const ExternalRedirect = (props) => {
  // first deconstruct the props
  const { isPrivate, link } = props;
  // grab user details
  const { user } = useUserContext();

  // this method is used to redirect the browser to the desired URL
  const redirectBrowser = (link) => {
    window.location.href = link;
    return null;
  };

  // define the return if isPrivate is false
  const normalReturnValue = redirectBrowser(routes.frontend.denied);

  // define the return if isPrivate is true
  const privateReturnValue = redirectBrowser(link);

  const canDisplayPrivateContent = isPrivate && user && user.isAuthenticated;

  // determine which value to render
  const returnValue = canDisplayPrivateContent
    ? privateReturnValue
    : normalReturnValue;

  // return
  return returnValue;
};

ExternalRedirect.propTypes = {
  link: PropTypes.string.isRequired,
  isPrivate: PropTypes.bool,
};

ExternalRedirect.defaultProps = {
  isPrivate: false,
};

export default ExternalRedirect;
